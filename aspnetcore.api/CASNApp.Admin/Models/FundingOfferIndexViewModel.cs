using System.Collections.Generic;
using CASNApp.Core.Entities;

namespace CASNApp.Admin.Models
{
    public class FundingOfferIndexViewModel : CallerLookupViewModel
    {
        public IEnumerable<FundingOffer> FundingOffers { get; set; }

    }
}
