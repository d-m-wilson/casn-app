using System.Linq;
using System.Threading.Tasks;
using CASNApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Admin.Controllers
{
    public class GrantsController : Controller
    {
        private readonly casn_appContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public GrantsController(casn_appContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Grants
        public async Task<IActionResult> Index()
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            return View(await _context.Grants.ToListAsync());
        }

        // GET: Grants/Details/5
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

            var grant = await _context.Grants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grant == null)
            {
                return NotFound();
            }

            return View(grant);
        }

        // GET: Grants/Create
        public async Task<IActionResult> Create()
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            return View();
        }

        // POST: Grants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Grant grant)
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                grant.IsActive = true;
                _context.Add(grant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grant);
        }

        // GET: Grants/Edit/5
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

            var grant = await _context.Grants.FindAsync(id);
            if (grant == null)
            {
                return NotFound();
            }
            return View(grant);
        }

        // POST: Grants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsActive")] Grant grant)
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            if (id != grant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrantExists(grant.Id))
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
            return View(grant);
        }

        // GET: Grants/Delete/5
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

            var grant = await _context.Grants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grant == null)
            {
                return NotFound();
            }

            return View(grant);
        }

        // POST: Grants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            var grant = await _context.Grants.FindAsync(id);
            _context.Grants.Remove(grant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrantExists(int id)
        {
            return _context.Grants.Any(e => e.Id == id);
        }

        private async Task<bool> UserHas2FA()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.TwoFactorEnabled;
        }

    }
}
