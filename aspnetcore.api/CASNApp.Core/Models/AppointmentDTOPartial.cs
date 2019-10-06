namespace CASNApp.Core.Models
{
    public partial class AppointmentDTO
    {
        public bool Validate()
        {
            if (Appointment == null || !Appointment.Validate())
                return false;

            if (DriveTo == null && DriveFrom == null)
                return false;

            if (DriveTo != null && !DriveTo.Validate(Drive.DirectionToServiceProvider))
                return false;

            if (DriveFrom != null && !DriveFrom.Validate(Drive.DirectionFromServiceProvider))
                return false;

            return true;
        }

        public void Redact(Entities.Volunteer volunteer)
        {
            var driveToAuthorized = DriveTo != null &&
                                    DriveTo.StatusId == Drive.StatusApproved &&
                                    DriveTo.DriverId.HasValue &&
                                    DriveTo.DriverId == volunteer.Id;

            var driveFromAuthorized = DriveFrom != null &&
                                      DriveFrom.StatusId == Drive.StatusApproved &&
                                      DriveFrom.DriverId.HasValue &&
                                      DriveFrom.DriverId == volunteer.Id;

            if (!driveToAuthorized && !driveFromAuthorized)
            {
                Caller?.Redact();
                Appointment?.Redact();
            }

            if (!driveToAuthorized)
            {
                DriveTo?.Redact(Appointment);
            }

            if (!driveFromAuthorized)
            {
                DriveFrom?.Redact(Appointment);
            }
        }

    }
}
