using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class VoucherItem
    {
        public int Id { get; set; }
        public int VoucherId { get; set; }
        public int FundingSourceId { get; set; }
        public int? FundingTypeId { get; set; }
        public decimal Amount { get; set; }
        public int? PaymentMethodId { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual FundingSource FundingSource { get; set; }
        public virtual FundingType FundingType { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual Voucher Voucher { get; set; }
    }
}
