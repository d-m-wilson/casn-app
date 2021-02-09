using System;
using System.ComponentModel.DataAnnotations;

namespace CASNApp.Core.Entities
{
    public sealed class CallerMetadata
    {
        public int Id { get; set; }

        public int CiviContactId { get; set; }

        [Required]
        public string CallerIdentifier { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\d+", ErrorMessage = "Phone must contain one or more digits.")]
        public string Phone { get; set; }

        public bool IsMinor { get; set; }

        [Required]
        [RegularExpression(@"\S+", ErrorMessage = "Preferred Language must not consist solely of spaces, tabs, etc.")]
        [Display(Name = "Preferred Language")]
        public string PreferredLanguage { get; set; }

        [Required]
        public short PreferredContactMethod { get; set; }

        public string Note { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        public string ResidencePostalCode { get; set; }


    }
}
