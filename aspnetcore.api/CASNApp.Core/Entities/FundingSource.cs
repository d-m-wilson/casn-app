using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class FundingSource
    {
        public FundingSource()
        {
            VoucherItems = new HashSet<VoucherItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsExternal { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual ICollection<VoucherItem> VoucherItems { get; set; }
    }
}
