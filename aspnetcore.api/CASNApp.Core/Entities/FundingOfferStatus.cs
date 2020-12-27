using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class FundingOfferStatus
    {
        public const int Draft = 1;
        public const int Issued = 2;
        public const int Redeemed = 3;
        public const int Paid = 4;
        public const int Voided = 5;

        public FundingOfferStatus()
        {
            FundingOffers = new HashSet<FundingOffer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual ICollection<FundingOffer> FundingOffers { get; set; }
    }
}
