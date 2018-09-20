namespace CASNApp.API.Models
{
    public partial class Clinic
    {
        public Clinic()
        {
        }

        public Clinic(Entities.Clinic e)
        {
            Id = e.Id;
            CiviContactId = e.CiviContactId;
            Name = e.Name;
            Phone = e.Phone;
            Address = e.Address;
            City = e.City;
            State = e.State;
            PostalCode = e.PostalCode;
        }

    }
}
