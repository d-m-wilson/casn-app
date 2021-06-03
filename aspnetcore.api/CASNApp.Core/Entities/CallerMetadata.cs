using System;
using System.ComponentModel.DataAnnotations;

namespace CASNApp.Core.Entities
{
    public sealed class CallerMetadata
    {
        [Display(AutoGenerateField = false, AutoGenerateFilter = false)]
        public int Id { get; set; }

        [Display(AutoGenerateField = false, AutoGenerateFilter = false)]
        public int CiviContactId { get; set; }

        [Required]
        [Display(Name = "Caller Identifier")]
        public string CallerIdentifier { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\d+", ErrorMessage = "Phone must contain one or more digits.")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Display(Name = "Is a minor")]
        public bool IsMinor { get; set; }

        [Required]
        [RegularExpression(@"\S+", ErrorMessage = "Preferred Language must not consist solely of spaces, tabs, etc.")]
        [Display(Name = "Preferred Language")]
        public string PreferredLanguage { get; set; }

        [Required]
        [Display(Name = "Preferred Contact Method")]
        public short PreferredContactMethod { get; set; }

        public string Note { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Residence Postal Code")]
        public string ResidencePostalCode { get; set; }

        [Display(Name = "Household Size")]
        public int? HouseholdSize { get; set; }

        [Display(Name = "Household Income")]
        public int? HouseholdIncome { get; set; }

        [Display(Name = "First Contact Date")]
        [DataType(DataType.Date)]
        public DateTime? FirstContactDate { get; set; }

        [MaxLength(2)]
        [Display(Name = "Residence State")]
        public string ResidenceState { get; set; }

        [Display(Name = "Referral Source")]
        public int? ReferralSourceId { get; set; }

        [Display(Name = "Housing Unstable")]
        public bool HousingUnstable { get; set; }

        [Display(Name = "Referral Source")]
        public ReferralSource ReferralSource { get; set; }

    }
}
