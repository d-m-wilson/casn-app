using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CASNApp.Admin.Models;
using CASNApp.Core.Entities;
using CASNApp.Core.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Admin.Controllers
{
    [Authorize]
    public class FundingOffersController : Controller
    {
        private readonly casn_appContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FundingOffersController(casn_appContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // if the user is not authenticated OR they don't have 2FA set up, deny them access
        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userHas2FA = await UserHas2FAAsync();

                if (userHas2FA)
                {
                    await base.OnActionExecutionAsync(context, next);
                    return;
                }
            }

            context.Result = Forbid();
        }

        // GET FundingOffers
        public async Task<IActionResult> Index()
        {
            var fundingOffers = _context.FundingOffers
                .AsNoTracking()
                .Include(f => f.Caller)
                .Include(f => f.Clinic)
                .Include(f => f.CreatedBy)
                .Include(f => f.FundingOfferStatus)
                .OrderByDescending(f => f.Id)
                .Take(50);

            var viewModel = new FundingOfferIndexViewModel()
            {
                FundingOffers = await fundingOffers.ToListAsync(),
            };

            return View(viewModel);
        }

        // GET: FundingOffers/List
        public async Task<IActionResult> List()
        {
            var casn_appContext = _context.FundingOffers
                .AsNoTracking()
                .Include(f => f.Caller)
                .Include(f => f.Clinic)
                .Include(f => f.CreatedBy)
                .Include(f => f.FundingOfferStatus);

            return View(await casn_appContext.ToListAsync());
        }

        // GET: FundingOffers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var fundingOffer = await _context.FundingOffers
                .Include(fo => fo.Caller)
                .Include(fo => fo.Clinic)
                .Include(fo => fo.CreatedBy)
                .Include(fo => fo.IssuedBy)
                .Include(fo => fo.AppointmentType)
                .Include(fo => fo.FundingOfferStatus)
                .Include(fo => fo.FundingOfferItems).ThenInclude(foi => foi.FundingSource)
                .Include(fo => fo.FundingOfferItems).ThenInclude(foi => foi.FundingType)
                .Include(fo => fo.FundingOfferItems).ThenInclude(foi => foi.PaymentMethod)
                .Include(fo => fo.FundingOfferItems).ThenInclude(foi => foi.NeedAmountNullReason)
                .Include(fo => fo.FundingOfferItems).ThenInclude(foi => foi.FundingAmountNullReason)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (fundingOffer == null)
            {
                return NotFound();
            }

            return View(fundingOffer);
        }

        // GET: FundingOffers/ItemDetails/5
        public async Task<IActionResult> ItemDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fundingOfferItem = await _context.FundingOfferItems
                .AsNoTracking()
                .Include(f => f.FundingAmountNullReason)
                .Include(f => f.FundingOffer)
                .Include(f => f.FundingSource)
                .Include(f => f.FundingType)
                .Include(f => f.NeedAmountNullReason)
                .Include(f => f.PaymentMethod)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (fundingOfferItem == null)
            {
                return NotFound();
            }

            return View(fundingOfferItem);
        }

        // GET: FundingOffers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var fundingOffer = await _context.FundingOffers.FindAsync(id);

            if (fundingOffer == null)
            {
                return NotFound();
            }

            ViewData["CallerId"] = new SelectList(_context.Callers, "Id", "CallerIdentifier", fundingOffer.CallerId);

            var serviceProviderQuery = new ServiceProviderQuery(_context);
            var activeClinics = await serviceProviderQuery.GetActiveClinicsAsync(true);
            ViewData["ClinicId"] = new SelectList(activeClinics, nameof(ServiceProvider.Id), nameof(ServiceProvider.Name), fundingOffer.ClinicId);

            var appointmentTypeQuery = new AppointmentTypeQuery(_context);
            var activeAppointmentTypes = await appointmentTypeQuery.GetActiveAppointmentTypesAsync(true);
            ViewData["AppointmentTypeId"] = new SelectList(activeAppointmentTypes, nameof(AppointmentType.Id), nameof(AppointmentType.Title), fundingOffer.AppointmentTypeId);

            ViewData["CreatedById"] = new SelectList(_context.Volunteers, "Id", "Name", fundingOffer.CreatedById);
            ViewData["FundingOfferStatusId"] = new SelectList(_context.FundingOfferStatuses, "Id", "Name", fundingOffer.FundingOfferStatusId);
            return View(fundingOffer);
        }

        // POST: FundingOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClinicId,AppointmentTypeId,AppointmentDate,Note,IsActive")] FundingOffer fundingOffer)
        {
            var volunteer = await GetVolunteerForCurrentUserAsync();

            if (volunteer == null)
            {
                return Forbid();
            }

            if (id != fundingOffer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingFundingOffer = await _context.FundingOffers.FindAsync(id);

                    if (existingFundingOffer == null)
                    {
                        return NotFound();
                    }

                    if (!existingFundingOffer.AllowsEdits)
                    {
                        return Conflict();
                    }

                    // prevent user from directly editing the status
                    fundingOffer.FundingOfferStatusId = existingFundingOffer.FundingOfferStatusId;

                    // prevent user from changing the CreatedById
                    fundingOffer.CreatedById = existingFundingOffer.CreatedById;

                    // prevent user from changing the CallerId
                    fundingOffer.CallerId = existingFundingOffer.CallerId;

                    fundingOffer.UpdatedById = volunteer.Id;

                    _context.Entry(existingFundingOffer).CurrentValues.SetValues(fundingOffer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FundingOfferExists(fundingOffer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = fundingOffer.Id });
            }

            ViewData["CallerId"] = new SelectList(_context.Callers, "Id", "CallerIdentifier", fundingOffer.CallerId);

            var serviceProviderQuery = new ServiceProviderQuery(_context);
            var activeClinics = await serviceProviderQuery.GetActiveClinicsAsync(true);
            ViewData["ClinicId"] = new SelectList(activeClinics, nameof(ServiceProvider.Id), nameof(ServiceProvider.Name), fundingOffer.ClinicId);

            var appointmentTypeQuery = new AppointmentTypeQuery(_context);
            var activeAppointmentTypes = await appointmentTypeQuery.GetActiveAppointmentTypesAsync(true);
            ViewData["AppointmentTypeId"] = new SelectList(activeAppointmentTypes, nameof(AppointmentType.Id), nameof(AppointmentType.Title), fundingOffer.AppointmentTypeId);

            ViewData["CreatedById"] = new SelectList(_context.Volunteers, "Id", "FirstName", fundingOffer.CreatedById);
            ViewData["FundingOfferStatusId"] = new SelectList(_context.FundingOfferStatuses, "Id", "Name", fundingOffer.FundingOfferStatusId);
            return View(fundingOffer);
        }

        // GET: FundingOffers/EditItem/5
        public async Task<IActionResult> EditItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fundingOfferItem = await _context.FundingOfferItems.FindAsync(id);

            if (fundingOfferItem == null)
            {
                return NotFound();
            }

            ViewData["FundingAmountNullReasonId"] = new SelectList(_context.NullReasons, "Id", "Name", fundingOfferItem.FundingAmountNullReasonId);
            ViewData["FundingSourceId"] = new SelectList(_context.FundingSources, "Id", "Name", fundingOfferItem.FundingSourceId);
            ViewData["FundingTypeId"] = new SelectList(_context.FundingTypes, "Id", "Name", fundingOfferItem.FundingTypeId);
            ViewData["NeedAmountNullReasonId"] = new SelectList(_context.NullReasons, "Id", "Name", fundingOfferItem.NeedAmountNullReasonId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "Name", fundingOfferItem.PaymentMethodId);
            return View(fundingOfferItem);
        }

        // POST: FundingOffers/EditItem/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItem(int id, [Bind("Id,FundingSourceId,FundingTypeId,NeedAmount,NeedAmountNullReasonId,FundingAmount,FundingAmountNullReasonId,PaymentMethodId,IsActive")] FundingOfferItem fundingOfferItem)
        {
            var volunteer = await GetVolunteerForCurrentUserAsync();

            if (volunteer == null)
            {
                return Forbid();
            }

            if (id != fundingOfferItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingFundingOfferItem = await _context.FundingOfferItems.FindAsync(id);

                    if (existingFundingOfferItem == null)
                    {
                        return NotFound();
                    }

                    var existingFundingOffer = await _context.FundingOffers.FindAsync(existingFundingOfferItem.FundingOfferId);

                    if (!existingFundingOffer.AllowsEdits)
                    {
                        return Conflict();
                    }

                    // prevent user from "moving" this item to a different FundingOffer (parent) record
                    fundingOfferItem.FundingOfferId = existingFundingOfferItem.FundingOfferId;

                    existingFundingOffer.UpdatedById = volunteer.Id;
                    _context.Entry(existingFundingOfferItem).CurrentValues.SetValues(fundingOfferItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FundingOfferItemExists(fundingOfferItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = fundingOfferItem.FundingOfferId });
            }
            ViewData["FundingAmountNullReasonId"] = new SelectList(_context.NullReasons, "Id", "Name", fundingOfferItem.FundingAmountNullReasonId);
            ViewData["FundingSourceId"] = new SelectList(_context.FundingSources, "Id", "Name", fundingOfferItem.FundingSourceId);
            ViewData["FundingTypeId"] = new SelectList(_context.FundingTypes, "Id", "Name", fundingOfferItem.FundingTypeId);
            ViewData["NeedAmountNullReasonId"] = new SelectList(_context.NullReasons, "Id", "Name", fundingOfferItem.NeedAmountNullReasonId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "Name", fundingOfferItem.PaymentMethodId);
            return View(fundingOfferItem);
        }

        // GET: FundingOffers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fundingOffer = await _context.FundingOffers
                .Include(f => f.Caller)
                .Include(f => f.Clinic)
                .Include(f => f.CreatedBy)
                .Include(f => f.FundingOfferStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fundingOffer == null)
            {
                return NotFound();
            }

            return View(fundingOffer);
        }

        // POST: FundingOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volunteer = await GetVolunteerForCurrentUserAsync();

            if (volunteer == null)
            {
                return Forbid();
            }

            var fundingOffer = await _context.FundingOffers.FindAsync(id);
            fundingOffer.UpdatedById = volunteer.Id;
            _context.FundingOffers.Remove(fundingOffer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: FundingOffers/DeleteItem/5
        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fundingOfferItem = await _context.FundingOfferItems
                .Include(f => f.FundingAmountNullReason)
                .Include(f => f.FundingOffer)
                .Include(f => f.FundingSource)
                .Include(f => f.FundingType)
                .Include(f => f.NeedAmountNullReason)
                .Include(f => f.PaymentMethod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fundingOfferItem == null)
            {
                return NotFound();
            }

            return View(fundingOfferItem);
        }

        // POST: FundingOffers/DeleteItem/5
        [HttpPost, ActionName("DeleteItem")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteItemConfirmed(int id)
        {
            var volunteer = await GetVolunteerForCurrentUserAsync();

            if (volunteer == null)
            {
                return Forbid();
            }

            var fundingOfferItem = await _context.FundingOfferItems.FindAsync(id);

            if (fundingOfferItem == null)
            {
                return NotFound();
            }

            var existingFundingOffer = await _context.FundingOffers.FindAsync(fundingOfferItem.FundingOfferId);

            if (existingFundingOffer == null)
            {
                return NotFound();
            }

            if (!existingFundingOffer.AllowsEdits)
            {
                return Conflict();
            }

            existingFundingOffer.UpdatedById = volunteer.Id;
            _context.FundingOfferItems.Remove(fundingOfferItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET FundingOffers/CallerLookup
        public IActionResult CallerLookup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CallerLookup(CallerLookupViewModel callerLookupViewModel)
        {
            if (string.IsNullOrWhiteSpace(callerLookupViewModel?.CallerIdentifier))
            {
                return BadRequest();
            }

            var caller = await _context.Callers
                .AsNoTracking()
                .Where(c => c.CallerIdentifier == callerLookupViewModel.CallerIdentifier)
                .FirstOrDefaultAsync();

            if (caller != null)
            {
                return RedirectToAction(nameof(CallerConfirm), new { id = caller.Id });
            }

            return RedirectToAction(nameof(CallerCreate), new { callerIdentifier = callerLookupViewModel.CallerIdentifier });
        }

        // GET: FundingOffers/CallerCreate
        public IActionResult CallerCreate([FromQuery]string callerIdentifier)
        {
            var caller = new Caller()
            {
                CallerIdentifier = callerIdentifier,
            };
            return View(caller);
        }

        // POST: FundingOffers/CallerCreate
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CallerCreate([Bind("Id,CiviContactId,CallerIdentifier,FirstName,LastName,DateOfBirth,Phone,IsMinor,PreferredLanguage,PreferredContactMethod,Note,ResidencePostalCode")] Caller caller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CallerOffers), new { id = caller.Id });
            }
            return View(caller);
        }

        // GET: FundingOffers/CallerConfirm/5
        public async Task<IActionResult> CallerConfirm([FromRoute]int id)
        {
            var caller = await _context.Callers.AsNoTracking().Where(c => c.Id == id).SingleAsync();

            if (caller == null)
            {
                return NotFound();
            }

            return View(caller);
        }

        // POST FundingOffers/CallerConfirm/5
        [HttpPost, ActionName("CallerConfirm")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CallerConfirmPost([FromRoute]int id)
        {
            var caller = await _context.Callers.Where(c => c.Id == id).SingleAsync();

            if (caller == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(CallerOffers), new { id = caller.Id });
        }

        // GET: FundingOffers/CallerOffers/5
        public async Task<IActionResult> CallerOffers([FromRoute]int id)
        {
            var caller = await _context.Callers.Where(c => c.Id == id).SingleAsync();

            if (caller == null)
            {
                return NotFound();
            }

            var offers = await _context.FundingOffers
                .AsNoTracking()
                .Include(f => f.Caller)
                .Include(f => f.Clinic)
                .Include(f => f.CreatedBy)
                .Include(f => f.FundingOfferStatus)
                .Where(fo => fo.CallerId == caller.Id)
                .OrderByDescending(fo => fo.Id)
                .ToListAsync();

            ViewBag.CallerId = caller.Id;
            ViewBag.CallerIdentifier = caller.CallerIdentifier;

            return View(offers);
        }

        // GET: FundingOffers/CallerCreateOffer/5
        public async Task<IActionResult> CallerCreateOffer([FromRoute]int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var caller = await _context.Callers.Where(c => c.Id == id).SingleOrDefaultAsync();

            if (caller == null)
            {
                return NotFound();
            }

            ViewBag.CallerId = caller.Id;

            var serviceProviderQuery = new ServiceProviderQuery(_context);
            var activeClinics = await serviceProviderQuery.GetActiveClinicsAsync(true);
            ViewData["ClinicId"] = new SelectList(activeClinics, nameof(ServiceProvider.Id), nameof(ServiceProvider.Name));

            var appointmentTypeQuery = new AppointmentTypeQuery(_context);
            var activeAppointmentTypes = await appointmentTypeQuery.GetActiveAppointmentTypesAsync(true);
            ViewData["AppointmentTypeId"] = new SelectList(activeAppointmentTypes, nameof(AppointmentType.Id), nameof(AppointmentType.Title));

            return View();
        }

        // POST: FundingOffers/CallerCreateOffer
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CallerCreateOffer([Bind("CallerId,ClinicId,AppointmentTypeId,AppointmentDate,Note")] FundingOffer fundingOffer)
        {
            var volunteer = await GetVolunteerForCurrentUserAsync();

            if (volunteer == null)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                // prevent user from creating a FundingOffer in any status but Draft
                fundingOffer.FundingOfferStatusId = FundingOfferStatus.Draft;

                // prevent user from creating a FundingOffer marked as inactive
                fundingOffer.IsActive = true;

                fundingOffer.CreatedById = volunteer.Id;

                _context.Add(fundingOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = fundingOffer.Id });
            }

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            ViewBag.CallerId = fundingOffer.CallerId;

            var serviceProviderQuery = new ServiceProviderQuery(_context);
            var activeClinics = await serviceProviderQuery.GetActiveClinicsAsync(true);
            ViewData["ClinicId"] = new SelectList(activeClinics, nameof(ServiceProvider.Id), nameof(ServiceProvider.Name), fundingOffer.ClinicId);

            var appointmentTypeQuery = new AppointmentTypeQuery(_context);
            var activeAppointmentTypes = await appointmentTypeQuery.GetActiveAppointmentTypesAsync(true);
            ViewData["AppointmentTypeId"] = new SelectList(activeAppointmentTypes, nameof(AppointmentType.Id), nameof(AppointmentType.Title));

            return View(fundingOffer);
        }

        // GET: FundingOffers/CreateItem
        public async Task<IActionResult> CreateItem([FromRoute]int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var fundingOffer = await _context.FundingOffers.Where(fo => fo.Id == id).SingleOrDefaultAsync();

            if (fundingOffer == null)
            {
                return NotFound();
            }

            ViewData["FundingAmountNullReasonId"] = new SelectList(_context.NullReasons, nameof(NullReason.Id), nameof(NullReason.Name));
            ViewBag.FundingOfferId = fundingOffer.Id;
            ViewData["FundingSourceId"] = new SelectList(_context.FundingSources, nameof(FundingSource.Id), nameof(FundingSource.Name));
            ViewData["FundingTypeId"] = new SelectList(_context.FundingTypes, nameof(FundingType.Id), nameof(FundingType.Name));
            ViewData["NeedAmountNullReasonId"] = new SelectList(_context.NullReasons, nameof(NullReason.Id), nameof(NullReason.Name));
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, nameof(PaymentMethod.Id), nameof(PaymentMethod.Name));
            return View();
        }

        // POST: FundingOffers/CreateItem
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItem([Bind("FundingOfferId,FundingSourceId,FundingTypeId,NeedAmount,NeedAmountNullReasonId,FundingAmount,FundingAmountNullReasonId,PaymentMethodId")] FundingOfferItem fundingOfferItem)
        {
            var volunteer = await GetVolunteerForCurrentUserAsync();

            if (volunteer == null)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                var existingFundingOffer = await _context.FundingOffers.FindAsync(fundingOfferItem.FundingOfferId);

                if (existingFundingOffer == null)
                {
                    return NotFound();
                }

                if (!existingFundingOffer.AllowsEdits)
                {
                    return Conflict();
                }

                existingFundingOffer.UpdatedById = volunteer.Id;
                fundingOfferItem.IsActive = true;
                _context.Add(fundingOfferItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = fundingOfferItem.FundingOfferId });
            }

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var fundingOffer = await _context.FundingOffers.Where(fo => fo.Id == fundingOfferItem.FundingOfferId).SingleOrDefaultAsync();

            ViewData["FundingAmountNullReasonId"] = new SelectList(_context.NullReasons, nameof(NullReason.Id), nameof(NullReason.Name), fundingOfferItem.FundingAmountNullReasonId);
            ViewBag.FundingOfferId = fundingOffer.Id;
            ViewData["FundingSourceId"] = new SelectList(_context.FundingSources, nameof(FundingSource.Id), nameof(FundingSource.Name), fundingOfferItem.FundingSourceId);
            ViewData["FundingTypeId"] = new SelectList(_context.FundingTypes, nameof(FundingType.Id), nameof(FundingType.Name), fundingOfferItem.FundingTypeId);
            ViewData["NeedAmountNullReasonId"] = new SelectList(_context.NullReasons, nameof(NullReason.Id), nameof(NullReason.Name), fundingOfferItem.NeedAmountNullReasonId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, nameof(PaymentMethod.Id), nameof(PaymentMethod.Name), fundingOfferItem.PaymentMethodId);
            return View(fundingOfferItem);
        }

        // GET: FundingOffers/Voucher/5
        public async Task<IActionResult> Voucher([FromRoute]int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var fundingOffer = await _context.FundingOffers
                .Include(fo => fo.Caller)
                .Include(fo => fo.Clinic)
                .Include(fo => fo.IssuedBy)
                .Include(fo => fo.FundingOfferStatus)
                .Include(fo => fo.AppointmentType)
                .Include(fo => fo.FundingOfferItems).ThenInclude(foi => foi.FundingSource)
                .Include(fo => fo.FundingOfferItems).ThenInclude(foi => foi.FundingType)
                .Include(fo => fo.FundingOfferItems).ThenInclude(foi => foi.PaymentMethod)
                .Include(fo => fo.FundingOfferItems).ThenInclude(foi => foi.NeedAmountNullReason)
                .Include(fo => fo.FundingOfferItems).ThenInclude(foi => foi.FundingAmountNullReason)
                .SingleOrDefaultAsync(fo => fo.Id == id.Value);

            if (fundingOffer == null)
            {
                return NotFound();
            }

            return View(fundingOffer);
        }

        // GET: FundingOffers/ChangeStatus/5
        public async Task<IActionResult> ChangeStatus([FromRoute]int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var fundingOffer = await _context.FundingOffers
                .Include(fo => fo.Caller)
                .Include(fo => fo.Clinic)
                .Include(fo => fo.CreatedBy)
                .Include(fo => fo.FundingOfferStatus)
                .Include(fo => fo.FundingOfferItems).ThenInclude(foi => foi.FundingSource)
                .Include(fo => fo.FundingOfferItems).ThenInclude(foi => foi.FundingType)
                .Include(fo => fo.FundingOfferItems).ThenInclude(foi => foi.PaymentMethod)
                .Include(fo => fo.FundingOfferItems).ThenInclude(foi => foi.NeedAmountNullReason)
                .Include(fo => fo.FundingOfferItems).ThenInclude(foi => foi.FundingAmountNullReason)
                .SingleOrDefaultAsync(fo => fo.Id == id.Value);

            if (fundingOffer == null)
            {
                return NotFound();
            }

            var validStatuses = await GetValidStatusesAsync(fundingOffer.FundingOfferStatusId);
            ViewData["FundingOfferStatusId"] = new SelectList(validStatuses, nameof(FundingOfferStatus.Id), nameof(FundingOfferStatus.Name));

            string warningText;

            if (fundingOffer.FundingOfferStatusId == FundingOfferStatus.Draft)
            {
                warningText = "Note: Once a Funding Offer's status is changed, no further editing will be permitted.";
            }
            else
            {
                warningText = "Note: Status changes are not reversible. Please be sure before you proceed.";
            }

            ViewData["WarningText"] = warningText;

            return View(fundingOffer);
        }

        // POST: FundingOffers/ChangeStatus/5
        [HttpPost, ActionName("ChangeStatus")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatusConfirmed([FromRoute] int id, [FromForm] int FundingOfferStatusId)
        {
            var volunteer = await GetVolunteerForCurrentUserAsync();

            if (volunteer == null)
            {
                return Forbid();
            }

            var fundingOffer = await _context.FundingOffers.FindAsync(id);

            if (fundingOffer == null)
            {
                return NotFound();
            }

            if (!FundingOfferStatus.IsValidStateTransition(fundingOffer.FundingOfferStatusId, FundingOfferStatusId))
            {
                return Conflict();
            }

            try
            {
                fundingOffer.FundingOfferStatusId = FundingOfferStatusId;
                fundingOffer.SetVolunteerId(FundingOfferStatusId, volunteer.Id);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FundingOfferExists(fundingOffer.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Details), new { id = fundingOffer.Id });
        }

        private Task<List<FundingOfferStatus>> GetValidStatusesAsync(int currentFundingOfferStatusId)
        {
            if (!FundingOfferStatus.ValidStateTransitions.ContainsKey(currentFundingOfferStatusId))
            {
                return Task.FromResult(new List<FundingOfferStatus>());
            }

            var validStateTransitions = FundingOfferStatus.ValidStateTransitions[currentFundingOfferStatusId];

            if (validStateTransitions == null || validStateTransitions.Count == 0)
            {
                return Task.FromResult(new List<FundingOfferStatus>());
            }

            return _context.FundingOfferStatuses
                .AsNoTracking()
                .Where(fos => validStateTransitions.Contains(fos.Id) && fos.IsActive)
                .ToListAsync();
        }

        private bool FundingOfferExists(int id)
        {
            return _context.FundingOffers.Any(e => e.Id == id);
        }

        private bool FundingOfferItemExists(int id)
        {
            return _context.FundingOfferItems.Any(e => e.Id == id);
        }

        private async Task<Volunteer> GetVolunteerForCurrentUserAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var volunteerQuery = new Core.Queries.VolunteerQuery(_context);
            var volunteer = await volunteerQuery.GetVolunteerByEmailAsync(user.Email, true);

            return volunteer;
        }

        private async Task<bool> UserHas2FAAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.TwoFactorEnabled;
        }

    }
}
