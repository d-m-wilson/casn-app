using System;
using System.Collections.Generic;

namespace dotnetcore.api.Entities
{
    public partial class Volunteer
    {
        public Volunteer()
        {
            Appointment = new HashSet<Appointment>();
            Drive = new HashSet<Drive>();
            VolunteerDrive = new HashSet<VolunteerDrive>();
        }

        public uint Id { get; set; }
        public uint CiviContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
        public string GoogleAccount { get; set; }
        public bool IsDriver { get; set; }
        public bool IsDispatcher { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? IsActive { get; set; }

        public ICollection<Appointment> Appointment { get; set; }
        public ICollection<Drive> Drive { get; set; }
        public ICollection<VolunteerDrive> VolunteerDrive { get; set; }
    }
}
