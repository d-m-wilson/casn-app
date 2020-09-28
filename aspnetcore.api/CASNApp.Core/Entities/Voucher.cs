using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class Voucher
    {
        public Voucher()
        {
            VoucherItems = new HashSet<VoucherItem>();
        }

        public int Id { get; set; }
        public int CallerId { get; set; }
        public int VoucherStatusId { get; set; }
        public int ClinicId { get; set; }
        public int CreatedById { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Issued { get; set; }
        public DateTime? Redeemed { get; set; }
        public DateTime? Paid { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Caller Caller { get; set; }
        public virtual ServiceProvider Clinic { get; set; }
        public virtual Volunteer CreatedBy { get; set; }
        public virtual VoucherStatus VoucherStatus { get; set; }
        public virtual ICollection<VoucherItem> VoucherItems { get; set; }
    }
}
