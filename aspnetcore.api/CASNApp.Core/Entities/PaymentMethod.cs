using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            FundingOfferItems = new HashSet<FundingOfferItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual ICollection<FundingOfferItem> FundingOfferItems { get; set; }
    }
}
