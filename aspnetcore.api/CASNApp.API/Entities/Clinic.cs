using System;
using System.Collections.Generic;

namespace CASNApp.API.Entities
{
    public partial class Clinic
    {
        public Clinic()
        {
            Appointments = new HashSet<Appointment>();
        }

        public uint Id { get; set; }
        public uint CiviContactId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
