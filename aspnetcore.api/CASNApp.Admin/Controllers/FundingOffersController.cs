using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CASNApp.Core.Entities;

namespace CASNApp.Admin.Controllers
{
    public class FundingOffersController : Controller
    {
        private readonly casn_appContext _context;

        public FundingOffersController(casn_appContext context)
        {
            _context = context;
        }

        // GET: FundingOffers
        public async Task<IActionResult> Index()
        {
            var casn_appContext = _context.FundingOffers.Include(f => f.Caller).Include(f => f.Clinic).Include(f => f.CreatedBy).Include(f => f.FundingOfferStatus);
            return View(await casn_appContext.ToListAsync());
        }

        // GET: FundingOffers/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: FundingOffers/Create
        public IActionResult Create()
        {
            ViewData["CallerId"] = new SelectList(_context.Callers, "Id", "CallerIdentifier");
            ViewData["ClinicId"] = new SelectList(_context.ServiceProviders, "Id", "Address");
            ViewData["CreatedById"] = new SelectList(_context.Volunteers, "Id", "FirstName");
            ViewData["FundingOfferStatusId"] = new SelectList(_context.FundingOfferStatuses, "Id", "Name");
            return View();
        }

        // POST: FundingOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CallerId,FundingOfferStatusId,ClinicId,CreatedById,IsActive,Created,Issued,Redeemed,Paid,Updated")] FundingOffer fundingOffer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fundingOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CallerId"] = new SelectList(_context.Callers, "Id", "CallerIdentifier", fundingOffer.CallerId);
            ViewData["ClinicId"] = new SelectList(_context.ServiceProviders, "Id", "Address", fundingOffer.ClinicId);
            ViewData["CreatedById"] = new SelectList(_context.Volunteers, "Id", "FirstName", fundingOffer.CreatedById);
            ViewData["FundingOfferStatusId"] = new SelectList(_context.FundingOfferStatuses, "Id", "Name", fundingOffer.FundingOfferStatusId);
            return View(fundingOffer);
        }

        // GET: FundingOffers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fundingOffer = await _context.FundingOffers.FindAsync(id);
            if (fundingOffer == null)
            {
                return NotFound();
            }
            ViewData["CallerId"] = new SelectList(_context.Callers, "Id", "CallerIdentifier", fundingOffer.CallerId);
            ViewData["ClinicId"] = new SelectList(_context.ServiceProviders, "Id", "Address", fundingOffer.ClinicId);
            ViewData["CreatedById"] = new SelectList(_context.Volunteers, "Id", "FirstName", fundingOffer.CreatedById);
            ViewData["FundingOfferStatusId"] = new SelectList(_context.FundingOfferStatuses, "Id", "Name", fundingOffer.FundingOfferStatusId);
            return View(fundingOffer);
        }

        // POST: FundingOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CallerId,FundingOfferStatusId,ClinicId,CreatedById,IsActive,Created,Issued,Redeemed,Paid,Updated")] FundingOffer fundingOffer)
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["CallerId"] = new SelectList(_context.Callers, "Id", "CallerIdentifier", fundingOffer.CallerId);
            ViewData["ClinicId"] = new SelectList(_context.ServiceProviders, "Id", "Address", fundingOffer.ClinicId);
            ViewData["CreatedById"] = new SelectList(_context.Volunteers, "Id", "FirstName", fundingOffer.CreatedById);
            ViewData["FundingOfferStatusId"] = new SelectList(_context.FundingOfferStatuses, "Id", "Name", fundingOffer.FundingOfferStatusId);
            return View(fundingOffer);
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

        private bool FundingOfferExists(int id)
        {
            return _context.FundingOffers.Any(e => e.Id == id);
        }
    }
}
