using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CASNApp.API.Models
{
    public partial class AppointmentDTO
    {
        public bool Validate()
        {
            if (Appointment == null || !Appointment.Validate())
                return false;

            if (DriveTo != null && !DriveTo.Validate(Drive.DirectionToClinic))
                return false;

            if (DriveFrom != null && !DriveFrom.Validate(Drive.DirectionFromClinic))
                return false;

            return true;
        }

    }
}
