using System;
using System.Collections.Generic;

namespace dotnetcore.api.Entities
{
    public partial class Appointment
    {
        public Appointment()
        {
            Drive = new HashSet<Drive>();
        }

        public uint Id { get; set; }
        public uint DispatcherId { get; set; }
        public uint PatientId { get; set; }
        public uint ClinicId { get; set; }
        public string PickupLocationVague { get; set; }
        public string DropoffLocationVague { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int AppointmentTypeId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public Volunteer Dispatcher { get; set; }
        public Patient Patient { get; set; }
        public ICollection<Drive> Drive { get; set; }
    }
}
