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
            ServiceProviderId = a.ServiceProviderId;
            Created = a.Created;
            DispatcherId = a.DispatcherId;
            DropoffLocationVague = a.DropoffLocationVague;
            DropoffVagueLatitude = a.DropoffVagueLatitude;
            DropoffVagueLongitude = a.DropoffVagueLongitude;
            Id = a.Id;
            CallerId = a.CallerId;
            PickupLocationVague = a.PickupLocationVague;
            PickupVagueLatitude = a.PickupVagueLatitude;
            PickupVagueLongitude = a.PickupVagueLongitude;
            Updated = a.Updated;
        }

        public bool Validate()
        {
            if (!CallerId.HasValue)
                return false;

            if (!ServiceProviderId.HasValue)
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

        public void Redact()
        {
        }

    }
}
