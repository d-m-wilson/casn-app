namespace CASNApp.Core.Models
{
    public partial class ServiceProvider
    {
        public ServiceProvider()
        {
        }

        public ServiceProvider(Entities.ServiceProvider e)
        {
            Id = e.Id;
            CiviContactId = e.CiviContactId;
            ServiceProviderTypeId = e.ServiceProviderTypeId;
            ServiceProviderType = e.ServiceProviderType?.Name;
            Name = e.Name;
            Phone = e.Phone;
            Address = e.Address;
            City = e.City;
            State = e.State;
            PostalCode = e.PostalCode;
            Latitude = e.Latitude;
            Longitude = e.Longitude;
        }

    }
}
