using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CASNApp.API.Models
{
    public partial class Appointment
    {
        public Appointment() { }

        public Appointment(Entities.Appointment a)
        {
            AppointmentDate = a.AppointmentDate;
            AppointmentTypeId = a.AppointmentTypeId;
            ClinicId = a.ClinicId;
            Created = a.Created;
            DispatcherId = a.DispatcherId;
            DropoffLocationVague = a.DropoffLocationVague;
            Id = a.Id;
            CallerId = a.CallerId;
            CallerIdentifier = a.Caller.CallerIdentifier;
            CallerNote = a.Caller.Note;
            PickupLocationVague = a.PickupLocationVague;
            Updated = a.Updated;
        }

        public bool Validate()
        {
            if (!CallerId.HasValue)
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
