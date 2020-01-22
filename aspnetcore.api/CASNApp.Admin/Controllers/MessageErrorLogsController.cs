using System.Linq;
using System.Threading.Tasks;
using CASNApp.Admin.Models;
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

            var allVolunteerNames = await _context.Volunteer
                .AsNoTracking()
                .Select(v => new { v.Id, Name = $"{v.FirstName} {v.LastName}" })
                .ToDictionaryAsync(x => x.Id, x => x.Name);

            var messageErrorLogs = await _context.MessageErrorLog
                .AsNoTracking()
                .OrderByDescending(mel => mel.DateSent)
                .Take(50)
                .OrderBy(mel => mel.DateSent)
                .ToListAsync();

            var results = messageErrorLogs
                .Select(mel => new MessageErrorLogIndexViewModel
                {
                    Id = mel.Id,
                    FromPhone = mel.FromPhone,
                    ToPhone = mel.ToPhone,
                    DateSent = mel.DateSent,
                    VolunteerName = (mel.VolunteerId.HasValue && allVolunteerNames.ContainsKey(mel.VolunteerId.Value)) ? allVolunteerNames[mel.VolunteerId.Value] : $"{mel.VolunteerId}",
                    ErrorCode = mel.ErrorCode,
                    ErrorMessage = mel.ErrorMessage,
                    Body = mel.Body
                })
                .ToList();

            return View(results);
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
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (messageErrorLog == null)
            {
                return NotFound();
            }

            var result = new MessageErrorLogDetailsViewModel
            {
                Id = messageErrorLog.Id,
                FromPhone = messageErrorLog.FromPhone,
                ToPhone = messageErrorLog.ToPhone,
                Subject = messageErrorLog.Subject,
                Body = messageErrorLog.Body,
                DateSent = messageErrorLog.DateSent,
                AppointmentId = messageErrorLog.AppointmentId,
                VolunteerId = messageErrorLog.VolunteerId,
                ErrorCode = messageErrorLog.ErrorCode,
                ErrorMessage = messageErrorLog.ErrorMessage,
                ErrorDetails = messageErrorLog.ErrorDetails,
            };

            if (messageErrorLog.VolunteerId.HasValue)
            {
                var volunteer = await _context.Volunteer
                    .AsNoTracking()
                    .FirstOrDefaultAsync(v => v.Id == messageErrorLog.VolunteerId.Value);

                if (volunteer != null)
                {
                    result.VolunteerName = $"{volunteer.FirstName} {volunteer.LastName}";
                }
                else
                {
                    result.VolunteerName = $"{messageErrorLog.VolunteerId}";
                }
            }

            return View(result);
        }

        private async Task<bool> UserHas2FA()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.TwoFactorEnabled;
        }

    }
}
