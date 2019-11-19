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
    public class ServiceProvidersController : Controller
    {
        private readonly casn_appContext _context;

        public ServiceProvidersController(casn_appContext context)
        {
            _context = context;
        }

        // GET: ServiceProviders
        public async Task<IActionResult> Index()
        {
            var casn_appContext = _context.ServiceProvider.Include(s => s.ServiceProviderType);
            return View(await casn_appContext.ToListAsync());
        }

        // GET: ServiceProviders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceProvider = await _context.ServiceProvider
                .Include(s => s.ServiceProviderType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceProvider == null)
            {
                return NotFound();
            }

            return View(serviceProvider);
        }

        // GET: ServiceProviders/Create
        public IActionResult Create()
        {
            ViewData["ServiceProviderTypeId"] = new SelectList(_context.ServiceProviderType, "Id", "Name");
            return View();
        }

        // POST: ServiceProviders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CiviContactId,ServiceProviderTypeId,Name,Phone,Address,City,State,PostalCode,Latitude,Longitude,Geocoded,IsActive,Created,Updated")] ServiceProvider serviceProvider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceProvider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceProviderTypeId"] = new SelectList(_context.ServiceProviderType, "Id", "Name", serviceProvider.ServiceProviderTypeId);
            return View(serviceProvider);
        }

        // GET: ServiceProviders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceProvider = await _context.ServiceProvider.FindAsync(id);
            if (serviceProvider == null)
            {
                return NotFound();
            }
            ViewData["ServiceProviderTypeId"] = new SelectList(_context.ServiceProviderType, "Id", "Name", serviceProvider.ServiceProviderTypeId);
            return View(serviceProvider);
        }

        // POST: ServiceProviders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CiviContactId,ServiceProviderTypeId,Name,Phone,Address,City,State,PostalCode,Latitude,Longitude,Geocoded,IsActive,Created,Updated")] ServiceProvider serviceProvider)
        {
            if (id != serviceProvider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceProvider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceProviderExists(serviceProvider.Id))
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
            ViewData["ServiceProviderTypeId"] = new SelectList(_context.ServiceProviderType, "Id", "Name", serviceProvider.ServiceProviderTypeId);
            return View(serviceProvider);
        }

        // GET: ServiceProviders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceProvider = await _context.ServiceProvider
                .Include(s => s.ServiceProviderType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceProvider == null)
            {
                return NotFound();
            }

            return View(serviceProvider);
        }

        // POST: ServiceProviders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceProvider = await _context.ServiceProvider.FindAsync(id);
            _context.ServiceProvider.Remove(serviceProvider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceProviderExists(int id)
        {
            return _context.ServiceProvider.Any(e => e.Id == id);
        }
    }
}
