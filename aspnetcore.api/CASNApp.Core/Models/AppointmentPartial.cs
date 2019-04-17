namespace CASNApp.Core.Models
{
    public partial class Appointment
    {
        public Appointment() { }

        public Appointment(Entities.Appointment a)
        {
            AppointmentDate = a.AppointmentDate;
            AppointmentTypeId = a.AppointmentTypeId;
            AppointmentType = a.AppointmentType?.Name;
            ClinicId = a.ClinicId;
            Created = a.Created;
            DispatcherId = a.DispatcherId;
            DropoffLocationVague = a.DropoffLocationVague;
            DropoffVagueLatitude = a.DropoffVagueLatitude;
            Id = a.Id;
            CallerId = a.CallerId;
            CallerIdentifier = a.Caller.CallerIdentifier;
            CallerNote = a.Caller.Note;
            PickupLocationVague = a.PickupLocationVague;
            PickupVagueLatitude = a.PickupVagueLatitude;
            PickupVagueLongitude = a.PickupVagueLongitude;
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

            if (string.IsNullOrWhiteSpace(PickupLocationVague) &&
                string.IsNullOrWhiteSpace(DropoffLocationVague))
            {
                return false;
            }

            return true;
        }

    }
}
