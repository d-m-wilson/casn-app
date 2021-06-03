using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class Drive
    {
        public Drive()
        {
            VolunteerDriveLogs = new HashSet<VolunteerDriveLog>();
        }

        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public byte Direction { get; set; }
        public int StatusId { get; set; }
        public int? DriverId { get; set; }
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
        public int? ApprovedById { get; set; }
        public int? CancelReasonId { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual Volunteer Approver { get; set; }
        public virtual DriveCancelReason CancelReason { get; set; }
        public virtual Volunteer Driver { get; set; }
        public virtual DriveStatus Status { get; set; }
        public virtual ICollection<VolunteerDriveLog> VolunteerDriveLogs { get; set; }
    }
}
