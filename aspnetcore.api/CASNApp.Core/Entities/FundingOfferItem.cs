using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class FundingOfferItem
    {
        public int Id { get; set; }
        public int FundingOfferId { get; set; }
        public int FundingSourceId { get; set; }
        public int? FundingTypeId { get; set; }
        public decimal? NeedAmount { get; set; }
        public int? NeedAmountNullReasonId { get; set; }
        public decimal? FundingAmount { get; set; }
        public int? FundingAmountNullReasonId { get; set; }
        public int? PaymentMethodId { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public int? GrantId { get; set; }

        public virtual NullReason FundingAmountNullReason { get; set; }
        public virtual FundingOffer FundingOffer { get; set; }
        public virtual FundingSource FundingSource { get; set; }
        public virtual FundingType FundingType { get; set; }
        public virtual Grant Grant { get; set; }
        public virtual NullReason NeedAmountNullReason { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
