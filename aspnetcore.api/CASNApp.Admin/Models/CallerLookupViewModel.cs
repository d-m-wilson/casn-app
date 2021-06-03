using System.ComponentModel.DataAnnotations;

namespace CASNApp.Admin.Models
{
    public class CallerLookupViewModel
    {
        [Required]
        public string CallerIdentifier { get; set; }
    }
}
