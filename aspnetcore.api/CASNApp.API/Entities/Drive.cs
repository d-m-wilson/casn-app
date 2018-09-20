using System;
using System.Collections.Generic;

namespace CASNApp.API.Entities
{
    public partial class Drive
    {
        public Drive()
        {
            VolunteerDrive = new HashSet<VolunteerDrive>();
        }

        public uint Id { get; set; }
        public uint AppointmentId { get; set; }
        public byte Direction { get; set; }
        public uint? DriverId { get; set; }
        public string StartAddress { get; set; }
        public string StartCity { get; set; }
        public string StartState { get; set; }
        public string StartPostalCode { get; set; }
        public string EndAddress { get; set; }
        public string EndCity { get; set; }
        public string EndState { get; set; }
        public string EndPostalCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public Appointment Appointment { get; set; }
        public Volunteer Driver { get; set; }
        public ICollection<VolunteerDrive> VolunteerDrive { get; set; }
    }
}
