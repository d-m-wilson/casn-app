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
    public class NullReasonsController : Controller
    {
        private readonly casn_appContext _context;

        public NullReasonsController(casn_appContext context)
        {
            _context = context;
        }

        // GET: NullReasons
        public async Task<IActionResult> Index()
        {
            return View(await _context.NullReasons.ToListAsync());
        }

        // GET: NullReasons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nullReason = await _context.NullReasons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nullReason == null)
            {
                return NotFound();
            }

            return View(nullReason);
        }

        // GET: NullReasons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NullReasons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsActive,Created,Updated")] NullReason nullReason)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nullReason);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nullReason);
        }

        // GET: NullReasons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nullReason = await _context.NullReasons.FindAsync(id);
            if (nullReason == null)
            {
                return NotFound();
            }
            return View(nullReason);
        }

        // POST: NullReasons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsActive,Created,Updated")] NullReason nullReason)
        {
            if (id != nullReason.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nullReason);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NullReasonExists(nullReason.Id))
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
            return View(nullReason);
        }

        // GET: NullReasons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nullReason = await _context.NullReasons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nullReason == null)
            {
                return NotFound();
            }

            return View(nullReason);
        }

        // POST: NullReasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nullReason = await _context.NullReasons.FindAsync(id);
            _context.NullReasons.Remove(nullReason);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NullReasonExists(int id)
        {
            return _context.NullReasons.Any(e => e.Id == id);
        }
    }
}
