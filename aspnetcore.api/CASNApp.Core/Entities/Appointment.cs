using System;
using System.Collections.Generic;

namespace CASNApp.API.Entities
{
    public partial class Appointment
    {
        public Appointment()
        {
            Drives = new HashSet<Drive>();
        }

        public uint Id { get; set; }
        public uint DispatcherId { get; set; }
        public uint CallerId { get; set; }
        public uint ClinicId { get; set; }
        public string PickupLocationVague { get; set; }
        public decimal? PickupVagueLatitude { get; set; }
        public decimal? PickupVagueLongitude { get; set; }
        public DateTime? PickupVagueGeocoded { get; set; }
        public string DropoffLocationVague { get; set; }
        public decimal? DropoffVagueLatitude { get; set; }
        public decimal? DropoffVagueLongitude { get; set; }
        public DateTime? DropoffVagueGeocoded { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int AppointmentTypeId { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public Clinic Clinic { get; set; }
        public Volunteer Dispatcher { get; set; }
        public Caller Caller { get; set; }
        public ICollection<Drive> Drives { get; set; }
    }
}
