using CASNApp.Core.Interfaces;

namespace CASNApp.Core.Entities
{
    public partial class PaymentMethod : ISoftDelete, ICreatedDate, IUpdatedDate
    {
        public const int Voucher = 1;

    }
}
