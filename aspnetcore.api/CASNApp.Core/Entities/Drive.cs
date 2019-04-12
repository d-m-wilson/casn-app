using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class Drive
    {
        public Drive()
        {
            VolunteerDrives = new HashSet<VolunteerDrive>();
        }

        public uint Id { get; set; }
        public uint AppointmentId { get; set; }
        public byte Direction { get; set; }
        public uint StatusId { get; set; }
        public uint? DriverId { get; set; }
        public string StartAddress { get; set; }
        public string StartCity { get; set; }
        public string StartState { get; set; }
        public string StartPostalCode { get; set; }
        public decimal? StartLatitude { get; set; }
        public decimal? StartLongitude { get; set; }
        public DateTime? StartGeocoded { get; set; }
        public string EndAddress { get; set; }
        public string EndCity { get; set; }
        public string EndState { get; set; }
        public string EndPostalCode { get; set; }
        public decimal? EndLatitude { get; set; }
        public decimal? EndLongitude { get; set; }
        public DateTime? EndGeocoded { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Approved { get; set; }
        public uint? ApprovedById { get; set; }
        public uint? CancelReasonId { get; set; }

        public Appointment Appointment { get; set; }
        public Volunteer Driver { get; set; }
        public Volunteer Approver { get; set; }
        public DriveCancelReason CancelReason { get; set; }
        public DriveStatus Status { get; set; }
        public ICollection<VolunteerDrive> VolunteerDrives { get; set; }
    }
}
