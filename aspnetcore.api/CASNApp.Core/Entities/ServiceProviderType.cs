using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class ServiceProviderType
    {
        public ServiceProviderType()
        {
            ServiceProviders = new HashSet<ServiceProvider>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual ICollection<ServiceProvider> ServiceProviders { get; set; }
    }
}
