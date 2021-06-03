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
    public class FundingSourcesController : Controller
    {
        private readonly casn_appContext _context;

        public FundingSourcesController(casn_appContext context)
        {
            _context = context;
        }

        // GET: FundingSources
        public async Task<IActionResult> Index()
        {
            return View(await _context.FundingSources.ToListAsync());
        }

        // GET: FundingSources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fundingSource = await _context.FundingSources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fundingSource == null)
            {
                return NotFound();
            }

            return View(fundingSource);
        }

        // GET: FundingSources/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FundingSources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsExternal,IsActive,Created,Updated")] FundingSource fundingSource)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fundingSource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fundingSource);
        }

        // GET: FundingSources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fundingSource = await _context.FundingSources.FindAsync(id);
            if (fundingSource == null)
            {
                return NotFound();
            }
            return View(fundingSource);
        }

        // POST: FundingSources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsExternal,IsActive,Created,Updated")] FundingSource fundingSource)
        {
            if (id != fundingSource.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fundingSource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FundingSourceExists(fundingSource.Id))
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
            return View(fundingSource);
        }

        // GET: FundingSources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fundingSource = await _context.FundingSources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fundingSource == null)
            {
                return NotFound();
            }

            return View(fundingSource);
        }

        // POST: FundingSources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fundingSource = await _context.FundingSources.FindAsync(id);
            _context.FundingSources.Remove(fundingSource);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FundingSourceExists(int id)
        {
            return _context.FundingSources.Any(e => e.Id == id);
        }
    }
}
