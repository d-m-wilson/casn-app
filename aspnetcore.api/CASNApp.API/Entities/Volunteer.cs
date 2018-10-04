using System;
using System.Collections.Generic;

namespace CASNApp.API.Entities
{
    public partial class Volunteer
    {
        public Volunteer()
        {
            Appointments = new HashSet<Appointment>();
            Drives = new HashSet<Drive>();
            VolunteerDrives = new HashSet<VolunteerDrive>();
        }

        public uint Id { get; set; }
        public uint CiviContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
        public string GoogleAccount { get; set; }
        public string GoogleIdentityToken { get; set; }
        public bool IsDriver { get; set; }
        public bool IsDispatcher { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Drive> Drives { get; set; }
        public ICollection<VolunteerDrive> VolunteerDrives { get; set; }
    }
}
