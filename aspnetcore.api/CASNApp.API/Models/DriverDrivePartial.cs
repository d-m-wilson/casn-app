using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CASNApp.API.Models
{
    public partial class DriverDrive
    {
        public DriverDrive()
        {
        }

        public DriverDrive(Entities.VolunteerDrive vd)
        {
            AppointmentDate = vd.Drive.Appointment.AppointmentDate;
            AppointmentId = vd.Drive.AppointmentId;
            AppointmentTypeId = vd.Drive.Appointment.AppointmentTypeId;
            ClinicId = vd.Drive.Appointment.ClinicId;
            Direction = vd.Drive.Direction;
            EndLocation = vd.Drive.Appointment.DropoffLocationVague;
            Id = vd.Id;
            IsApproved = vd.Drive.Status == Drive.StatusApproved;
            PatientIdentifier = vd.Drive.Appointment.Patient.PatientIdentifier;
            PatientName = vd.Drive.Appointment.Patient.FirstName + " " + vd.Drive.Appointment.Patient.LastName;
            StartLocation = vd.Drive.Appointment.PickupLocationVague;
            VolunteerDriveId = vd.Id;
        }

    }
}
