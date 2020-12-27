using System.Linq;
using System.Threading.Tasks;
using CASNApp.Admin.Models;
using CASNApp.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Admin.Controllers
{
    [Authorize]
    public class FundingOffersController : Controller
    {
        private readonly casn_appContext _context;

        public FundingOffersController(casn_appContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var casn_appContext = _context.FundingOffers
                .AsNoTracking()
                .Include(f => f.Caller)
                .Include(f => f.Clinic)
                .Include(f => f.CreatedBy)
                .Include(f => f.FundingOfferStatus);

            var viewModel = new FundingOfferIndexViewModel()
            {
                FundingOffers = await casn_appContext.ToListAsync(),
            };

            return View(viewModel);
        }

        // GET: FundingOffers
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
                .Include(f => f.Caller)
                .Include(f => f.Clinic)
                .Include(f => f.CreatedBy)
                .Include(f => f.FundingOfferStatus)
                .Include(f => f.FundingOfferItems)
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

        // POST: FundingOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CallerId,FundingOfferStatusId,ClinicId,CreatedById,Note")] FundingOffer fundingOffer)
        {
            if (ModelState.IsValid)
            {
                // prevent user from creating a FundingOffer in any status but Draft
                var draftStatus = await _context.FundingOfferStatuses.AsNoTracking().SingleAsync(d => d.Id == FundingOfferStatus.Draft);
                fundingOffer.FundingOfferStatusId = draftStatus.Id;

                // prevent user from creating a FundingOffer marked as inactive
                fundingOffer.IsActive = true;

                _context.Add(fundingOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CallerOffers), new { id = fundingOffer.CallerId });
            }

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            ViewBag.CallerId = fundingOffer.CallerId;
            ViewData["ClinicId"] = new SelectList(_context.ServiceProviders, nameof(ServiceProvider.Id), nameof(ServiceProvider.Name), fundingOffer.ClinicId);
            ViewData["CreatedById"] = new SelectList(_context.Volunteers, "Id", "Name", fundingOffer.CreatedById);
            ViewData["FundingOfferStatusId"] = new SelectList(_context.FundingOfferStatuses.Where(s => s.Id == FundingOfferStatus.Draft), nameof(Caller.Id), nameof(Caller.Name));
            ViewBag.CurrentCallerId = fundingOffer.CallerId;
            return View(fundingOffer);
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
            ViewData["ClinicId"] = new SelectList(_context.ServiceProviders, "Id", "Name", fundingOffer.ClinicId);
            ViewData["CreatedById"] = new SelectList(_context.Volunteers, "Id", "Name", fundingOffer.CreatedById);
            ViewData["FundingOfferStatusId"] = new SelectList(_context.FundingOfferStatuses, "Id", "Name", fundingOffer.FundingOfferStatusId);
            return View(fundingOffer);
        }

        // POST: FundingOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CallerId,FundingOfferStatusId,ClinicId,CreatedById,Note,IsActive")] FundingOffer fundingOffer)
        {
            if (id != fundingOffer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fundingOffer);
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
                return RedirectToAction(nameof(CallerOffers), new { id = fundingOffer.CallerId });
            }

            ViewData["CallerId"] = new SelectList(_context.Callers, "Id", "CallerIdentifier", fundingOffer.CallerId);
            ViewData["ClinicId"] = new SelectList(_context.ServiceProviders, "Id", "Address", fundingOffer.ClinicId);
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
        public async Task<IActionResult> EditItem(int id, [Bind("Id,FundingOfferId,FundingSourceId,FundingTypeId,NeedAmount,NeedAmountNullReasonId,FundingAmount,FundingAmountNullReasonId,PaymentMethodId,IsActive")] FundingOfferItem fundingOfferItem)
        {
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

                    // prevent user from "moving" this item to a different FundingOffer (parent) record
                    fundingOfferItem.FundingOfferId = existingFundingOfferItem.FundingOfferId;

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
                return RedirectToAction(nameof(Index));
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
            var fundingOffer = await _context.FundingOffers.FindAsync(id);
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
            var fundingOfferItem = await _context.FundingOfferItems.FindAsync(id);
            _context.FundingOfferItems.Remove(fundingOfferItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

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

        // POST: GET: FundingOffers/CallerCreate
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

        public async Task<IActionResult> CallerConfirm([FromRoute]int id)
        {
            var caller = await _context.Callers.AsNoTracking().Where(c => c.Id == id).SingleAsync();

            if (caller == null)
            {
                return NotFound();
            }

            return View(caller);
        }

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
                .OrderBy(fo => fo.Created)
                .ToListAsync();

            ViewBag.CallerId = caller.Id;
            ViewBag.CallerIdentifier = caller.CallerIdentifier;

            return View(offers);
        }

        public async Task<IActionResult> CallerCreateOffer([FromRoute]int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var caller = await _context.Callers.Where(c => c.Id == id).SingleOrDefaultAsync();

            if (caller == null)
            {
                return NotFound();
            }

            ViewBag.CallerId = caller.Id;
            ViewData["ClinicId"] = new SelectList(_context.ServiceProviders, nameof(Caller.Id), nameof(Caller.Name));
            ViewData["CreatedById"] = new SelectList(_context.Volunteers, nameof(Caller.Id), nameof(Caller.Name));
            ViewData["FundingOfferStatusId"] = new SelectList(_context.FundingOfferStatuses.Where(s => s.Id == FundingOfferStatus.Draft), nameof(Caller.Id), nameof(Caller.Name));

            ViewBag.CurrentCallerId = caller.Id;

            return View();
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
            if (ModelState.IsValid)
            {
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

        private bool FundingOfferExists(int id)
        {
            return _context.FundingOffers.Any(e => e.Id == id);
        }

        private bool FundingOfferItemExists(int id)
        {
            return _context.FundingOfferItems.Any(e => e.Id == id);
        }

    }
}
