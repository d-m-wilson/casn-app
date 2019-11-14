using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CASNApp.Core.Models
{
    public partial class DriverDrive
    {
        public DriverDrive()
        {
        }

        public DriverDrive(Entities.VolunteerDriveLog vdl)
        {
            AppointmentDate = vdl.Drive.Appointment.AppointmentDate;
            AppointmentId = vdl.Drive.AppointmentId;
            AppointmentTypeId = vdl.Drive.Appointment.AppointmentTypeId;
            ServiceProviderId = vdl.Drive.Appointment.ServiceProviderId;
            Direction = vdl.Drive.Direction;
            EndLocation = vdl.Drive.Appointment.DropoffLocationVague;
            EndLatitude = vdl.Drive.Appointment.DropoffVagueLatitude;
            EndLongitude = vdl.Drive.Appointment.DropoffVagueLongitude;
            Id = vdl.Id;
            IsApproved = vdl.Drive.StatusId == Drive.StatusApproved;
            CallerIdentifier = vdl.Drive.Appointment.Caller.CallerIdentifier;
            CallerName = vdl.Drive.Appointment.Caller.FirstName + " " + vdl.Drive.Appointment.Caller.LastName;
            CallerNote = vdl.Drive.Appointment.Caller.Note;
            StartLocation = vdl.Drive.Appointment.PickupLocationVague;
            StartLatitude = vdl.Drive.Appointment.PickupVagueLatitude;
            StartLongitude = vdl.Drive.Appointment.PickupVagueLongitude;
            VolunteerDriveId = vdl.Id;
        }

    }
}
