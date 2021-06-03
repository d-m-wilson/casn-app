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
    public class FundingTypesController : Controller
    {
        private readonly casn_appContext _context;

        public FundingTypesController(casn_appContext context)
        {
            _context = context;
        }

        // GET: FundingTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.FundingTypes.ToListAsync());
        }

        // GET: FundingTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fundingType = await _context.FundingTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fundingType == null)
            {
                return NotFound();
            }

            return View(fundingType);
        }

        // GET: FundingTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FundingTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsActive,Created,Updated")] FundingType fundingType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fundingType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fundingType);
        }

        // GET: FundingTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fundingType = await _context.FundingTypes.FindAsync(id);
            if (fundingType == null)
            {
                return NotFound();
            }
            return View(fundingType);
        }

        // POST: FundingTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsActive,Created,Updated")] FundingType fundingType)
        {
            if (id != fundingType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fundingType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FundingTypeExists(fundingType.Id))
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
            return View(fundingType);
        }

        // GET: FundingTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fundingType = await _context.FundingTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fundingType == null)
            {
                return NotFound();
            }

            return View(fundingType);
        }

        // POST: FundingTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fundingType = await _context.FundingTypes.FindAsync(id);
            _context.FundingTypes.Remove(fundingType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FundingTypeExists(int id)
        {
            return _context.FundingTypes.Any(e => e.Id == id);
        }
    }
}
