using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CASNApp.Admin.Controllers
{
    [Authorize]
    public class ReportingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}