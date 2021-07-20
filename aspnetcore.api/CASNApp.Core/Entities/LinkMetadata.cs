using System;
using System.ComponentModel.DataAnnotations;

namespace CASNApp.Core.Entities
{
    public sealed class LinkMetadata
    {
        [Display(AutoGenerateField = false, AutoGenerateFilter = false)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "URL")]
        public string Url { get; set; }

        public string Target { get; set; }

        [Required]
        [Display(Name = "Display Ordinal")]
        public int DisplayOrdinal { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

    }
}
