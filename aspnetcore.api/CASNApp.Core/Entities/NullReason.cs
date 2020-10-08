using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class NullReason
    {
        public NullReason()
        {
            FundingOfferItemFundingAmountNullReasons = new HashSet<FundingOfferItem>();
            FundingOfferItemNeedAmountNullReasons = new HashSet<FundingOfferItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual ICollection<FundingOfferItem> FundingOfferItemFundingAmountNullReasons { get; set; }
        public virtual ICollection<FundingOfferItem> FundingOfferItemNeedAmountNullReasons { get; set; }
    }
}
