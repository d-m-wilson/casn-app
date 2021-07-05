using System.Linq;
using System.Threading.Tasks;
using CASNApp.Core.Entities;
using CASNApp.Core.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Admin.Controllers
{
    public class LinksController : Controller
    {
        private readonly casn_appContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public LinksController(casn_appContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Links
        public async Task<IActionResult> Index()
        {
            var volunteer = await GetVolunteerForCurrentUserAsync();
            var userCanSeeInactive = volunteer != null && volunteer.CanSeeInactive;

            var linkQuery = new LinkQuery(_context);
            var links = await linkQuery.GetLinksAsync(true, userCanSeeInactive);

            ViewBag.UserCanSeeInactive = userCanSeeInactive;

            return View(links);
        }

        // GET: Links/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await _context.Links
                .FirstOrDefaultAsync(m => m.Id == id);
            if (link == null)
            {
                return NotFound();
            }

            return View(link);
        }

        // GET: Links/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Links/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Url,Target,DisplayOrdinal")] Link link)
        {
            if (ModelState.IsValid)
            {
                link.IsActive = true;
                link.Target ??= "";
                _context.Add(link);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(link);
        }

        // GET: Links/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await _context.Links.FindAsync(id);
            if (link == null)
            {
                return NotFound();
            }
            return View(link);
        }

        // POST: Links/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Url,Target,DisplayOrdinal,IsActive")] Link link)
        {
            if (id != link.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    link.Target ??= "";
                    _context.Update(link);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinkExists(link.Id))
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
            return View(link);
        }

        // GET: Links/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await _context.Links
                .FirstOrDefaultAsync(m => m.Id == id);
            if (link == null)
            {
                return NotFound();
            }

            return View(link);
        }

        // POST: Links/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var link = await _context.Links.FindAsync(id);
            _context.Links.Remove(link);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LinkExists(int id)
        {
            return _context.Links.Any(e => e.Id == id);
        }

        private async Task<Volunteer> GetVolunteerForCurrentUserAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var volunteerQuery = new Core.Queries.VolunteerQuery(_context);
            var volunteer = await volunteerQuery.GetVolunteerByEmailAsync(user.Email, true);

            return volunteer;
        }

    }
}
