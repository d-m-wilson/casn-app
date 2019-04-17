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

            if (DriveTo != null && !DriveTo.Validate(Drive.DirectionToClinic))
                return false;

            if (DriveFrom != null && !DriveFrom.Validate(Drive.DirectionFromClinic))
                return false;

            return true;
        }

    }
}
