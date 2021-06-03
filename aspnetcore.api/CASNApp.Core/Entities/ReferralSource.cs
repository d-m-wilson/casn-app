using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class ReferralSource
    {
        public ReferralSource()
        {
            Callers = new HashSet<Caller>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual ICollection<Caller> Callers { get; set; }
    }
}
