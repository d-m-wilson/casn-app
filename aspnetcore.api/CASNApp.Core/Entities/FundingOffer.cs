using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class FundingOffer
    {
        public FundingOffer()
        {
            FundingOfferItems = new HashSet<FundingOfferItem>();
        }

        public int Id { get; set; }
        public int CallerId { get; set; }
        public int ClinicId { get; set; }
        public int CreatedById { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Issued { get; set; }
        public DateTime? Redeemed { get; set; }
        public DateTime? Paid { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Voided { get; set; }
        public int? IssuedById { get; set; }
        public int? VoidedById { get; set; }
        public int? UpdatedById { get; set; }

        public virtual Caller Caller { get; set; }
        public virtual ServiceProvider Clinic { get; set; }
        public virtual Volunteer CreatedBy { get; set; }
        public virtual FundingOfferStatus FundingOfferStatus { get; set; }
        public virtual Volunteer IssuedBy { get; set; }
        public virtual Volunteer UpdatedBy { get; set; }
        public virtual Volunteer VoidedBy { get; set; }
        public virtual ICollection<FundingOfferItem> FundingOfferItems { get; set; }
    }
}
