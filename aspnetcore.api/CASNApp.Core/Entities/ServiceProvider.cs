using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class ServiceProvider
    {
        public ServiceProvider()
        {
            Appointments = new HashSet<Appointment>();
            FundingOffers = new HashSet<FundingOffer>();
        }

        public int Id { get; set; }
        public int? CiviContactId { get; set; }
        public int ServiceProviderTypeId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTime? Geocoded { get; set; }

        public virtual ServiceProviderType ServiceProviderType { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<FundingOffer> FundingOffers { get; set; }
    }
}
