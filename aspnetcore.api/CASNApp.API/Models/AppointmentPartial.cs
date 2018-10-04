using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CASNApp.API.Models
{
    public partial class Appointment
    {
        public bool Validate()
        {
            if (!PatientId.HasValue)
                return false;

            if (!ClinicId.HasValue)
                return false;

            if (!DispatcherId.HasValue)
                return false;

            if (!AppointmentTypeId.HasValue)
                return false;

            if (!AppointmentDate.HasValue)
                return false;

            if (string.IsNullOrWhiteSpace(PickupLocationVague))
                return false;

            if (string.IsNullOrWhiteSpace(DropoffLocationVague))
                return false;

            return true;
        }

    }
}
