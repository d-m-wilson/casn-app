using System.Linq;
using System.Threading.Tasks;
using CASNApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Admin.Controllers
{
    public class ReferralSourcesController : Controller
    {
        private readonly casn_appContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReferralSourcesController(casn_appContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ReferralSources
        public async Task<IActionResult> Index()
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            return View(await _context.ReferralSources.ToListAsync());
        }

        // GET: ReferralSources/Details/5
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

            var referralSource = await _context.ReferralSources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (referralSource == null)
            {
                return NotFound();
            }

            return View(referralSource);
        }

        // GET: ReferralSources/Create
        public async Task<IActionResult> Create()
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            return View();
        }

        // POST: ReferralSources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ReferralSource referralSource)
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                referralSource.IsActive = true;
                _context.Add(referralSource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(referralSource);
        }

        // GET: ReferralSources/Edit/5
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

            var referralSource = await _context.ReferralSources.FindAsync(id);
            if (referralSource == null)
            {
                return NotFound();
            }
            return View(referralSource);
        }

        // POST: ReferralSources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsActive")] ReferralSource referralSource)
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            if (id != referralSource.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(referralSource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReferralSourceExists(referralSource.Id))
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
            return View(referralSource);
        }

        // GET: ReferralSources/Delete/5
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

            var referralSource = await _context.ReferralSources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (referralSource == null)
            {
                return NotFound();
            }

            return View(referralSource);
        }

        // POST: ReferralSources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            var referralSource = await _context.ReferralSources.FindAsync(id);
            _context.ReferralSources.Remove(referralSource);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReferralSourceExists(int id)
        {
            return _context.ReferralSources.Any(e => e.Id == id);
        }

        private async Task<bool> UserHas2FA()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.TwoFactorEnabled;
        }

    }
}
