using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class Appointment
    {
        public Appointment()
        {
            Drives = new HashSet<Drive>();
        }

        public int Id { get; set; }
        public int DispatcherId { get; set; }
        public int? CallerId { get; set; }
        public int ServiceProviderId { get; set; }
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
        public int? Tier1MessageCount { get; set; }
        public DateTime? Tier1MessageDate { get; set; }
        public int? Tier2MessageCount { get; set; }
        public DateTime? Tier2MessageDate { get; set; }
        public int? Tier3MessageCount { get; set; }
        public DateTime? Tier3MessageDate { get; set; }
        public int? BroadcastMessageCount { get; set; }
        public DateTime? BroadcastMessageDate { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsRedacted { get; set; }

        public virtual AppointmentType AppointmentType { get; set; }
        public virtual Caller Caller { get; set; }
        public virtual Volunteer Dispatcher { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
        public virtual ICollection<Drive> Drives { get; set; }
    }
}
