﻿using System.Linq;
using System.Threading.Tasks;
using CASNApp.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Admin.Controllers
{
    [Authorize]
    public class CallersController : Controller
    {
        private readonly casn_appContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CallersController(casn_appContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Callers
        public async Task<IActionResult> Index()
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            var userCanSeeInactive = await UserCanSeeInactive();

            var callersQuery = _context.Callers
                .Include(c => c.ReferralSource)
                .Where(c => userCanSeeInactive || c.IsActive);

            return View(await callersQuery.ToListAsync());
        }

        // GET: Callers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            if (id == null)
            {
                return NotFound();
            }

            var caller = await _context.Callers
                .Include(c => c.ReferralSource)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (caller == null)
            {
                return NotFound();
            }

            return View(caller);
        }

        // GET: Callers/Create
        public async Task<IActionResult> Create()
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            ViewData["ReferralSourceId"] = new SelectList(_context.ReferralSources, "Id", "Name");
            return View();
        }

        // POST: Callers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CiviContactId,CallerIdentifier,FirstName,LastName,DateOfBirth,Phone,IsMinor,PreferredLanguage,PreferredContactMethod,Note,ResidencePostalCode,HouseholdSize,HouseholdIncome,FirstContactDate,ResidenceState,ReferralSourceId,HousingUnstable")] Caller caller)
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                caller.IsActive = true;
                _context.Add(caller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = caller.Id });
            }
            ViewData["ReferralSourceId"] = new SelectList(_context.ReferralSources, "Id", "Name", caller.ReferralSourceId);
            return View(caller);
        }

        // GET: Callers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            if (id == null)
            {
                return NotFound();
            }

            var caller = await _context.Callers.FindAsync(id);
            if (caller == null)
            {
                return NotFound();
            }
            ViewData["ReferralSourceId"] = new SelectList(_context.ReferralSources, "Id", "Name", caller.ReferralSourceId);
            return View(caller);
        }

        // POST: Callers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CiviContactId,CallerIdentifier,FirstName,LastName,DateOfBirth,Phone,IsMinor,PreferredLanguage,PreferredContactMethod,Note,IsActive,ResidencePostalCode,HouseholdSize,HouseholdIncome,FirstContactDate,ResidenceState,ReferralSourceId,HousingUnstable")] Caller caller)
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            if (id != caller.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CallerExists(caller.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = caller.Id });
            }
            ViewData["ReferralSourceId"] = new SelectList(_context.ReferralSources, "Id", "Name", caller.ReferralSourceId);
            return View(caller);
        }

        // GET: Callers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            if (id == null)
            {
                return NotFound();
            }

            var caller = await _context.Callers
                .Include(c => c.ReferralSource)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caller == null)
            {
                return NotFound();
            }

            return View(caller);
        }

        // POST: Callers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            var caller = await _context.Callers.FindAsync(id);
            _context.Callers.Remove(caller);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CallerExists(int id)
        {
            return _context.Callers.Any(e => e.Id == id);
        }

        private async Task<bool> UserHas2FA()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.TwoFactorEnabled;
        }

        private async Task<Volunteer> GetVolunteerForCurrentUserAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var volunteerQuery = new Core.Queries.VolunteerQuery(_context);
            var volunteer = await volunteerQuery.GetVolunteerByEmailAsync(user.Email, true);

            return volunteer;
        }

        private async Task<bool> UserCanSeeInactive()
        {
            var volunteer = await GetVolunteerForCurrentUserAsync();

            if (volunteer != null && volunteer.CanSeeInactive)
            {
                return true;
            }

            return false;
        }

    }
}
