using System.Linq;
using System.Threading.Tasks;
using CASNApp.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Admin.Controllers
{
    [Authorize]
    public class MessageLogsController : Controller
    {
        private readonly casn_appContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MessageLogsController(casn_appContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: MessageLogs
        public async Task<IActionResult> Index()
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            return View(await _context.MessageLogs
                .OrderByDescending(ml => ml.Id)
                .Take(50)
                .OrderBy(ml => ml.Id)
                .ToListAsync());
        }

        // GET: MessageLogs/Details/5
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

            var messageLog = await _context.MessageLogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messageLog == null)
            {
                return NotFound();
            }

            return View(messageLog);
        }

        private async Task<bool> UserHas2FA()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.TwoFactorEnabled;
        }

    }
}
