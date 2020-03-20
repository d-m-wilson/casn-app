using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASNApp.Admin.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        public RegisterConfirmationModel()
        {
        }

        public Task<IActionResult> OnGetAsync(string email)
        {
            return Task.FromResult<IActionResult>(RedirectToPage("Login"));
        }
    }
}
