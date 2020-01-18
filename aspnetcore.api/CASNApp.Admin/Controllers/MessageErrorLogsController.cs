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
    public class MessageErrorLogsController : Controller
    {
        private readonly casn_appContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MessageErrorLogsController(casn_appContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: MessageErrorLogs
        public async Task<IActionResult> Index()
        {
            if (!await UserHas2FA())
            {
                return Forbid();
            }

            return View(await _context.MessageErrorLog
                .AsNoTracking()
                .OrderByDescending(mel => mel.DateSent)
                .Take(50)
                .OrderBy(ml => ml.DateSent)
                .ToListAsync());
        }

        // GET: MessageErrorLogs/Details/5
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

            var messageErrorLog = await _context.MessageErrorLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messageErrorLog == null)
            {
                return NotFound();
            }

            return View(messageErrorLog);
        }

        private async Task<bool> UserHas2FA()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.TwoFactorEnabled;
        }

    }
}
