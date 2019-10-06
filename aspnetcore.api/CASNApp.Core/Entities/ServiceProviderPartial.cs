using System;

namespace CASNApp.Core.Entities
{
    public partial class ServiceProvider
    {
        public string GetAddress()
        {
            return $"{Address}, {City}, {State} {PostalCode}";
        }

        public void SetLocation(Queries.GeocoderQuery.LatLng point)
        {
            Latitude = point.Latitude;
            Longitude = point.Longitude;
            Geocoded = DateTime.UtcNow;
        }

    }
}
