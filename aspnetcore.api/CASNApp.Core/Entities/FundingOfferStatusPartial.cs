using CASNApp.Core.Interfaces;

namespace CASNApp.Core.Entities
{
    public partial class FundingOfferStatus : ISoftDelete, ICreatedDate, IUpdatedDate
    {
        public const int Draft = 1;
        public const int Issued = 2;
        public const int Redeemed = 3;
        public const int Paid = 4;
        public const int Voided = 5;

    }
}
