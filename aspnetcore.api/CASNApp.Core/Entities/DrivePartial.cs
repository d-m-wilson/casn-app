using System;

namespace CASNApp.Core.Entities
{
    public partial class Drive
    {
        public string GetCallerAddress()
        {
            if (Direction == Models.Drive.DirectionToServiceProvider)
            {
                return $"{StartAddress}, {StartCity}, {StartState} {StartPostalCode}";
            }
            else if (Direction == Models.Drive.DirectionFromServiceProvider)
            {
                return $"{EndAddress}, {EndCity}, {EndState} {EndPostalCode}";
            }
            else
            {
                string errorMessage = $"Invalid value for {nameof(Drive)}.{nameof(Direction)}: {Direction}";
                throw new InvalidOperationException(errorMessage);
            }
        }

        public void SetCallerLocation(Queries.GeocoderQuery.LatLng point)
        {
            if (Direction == Models.Drive.DirectionToServiceProvider)
            {
                StartLatitude = point.Latitude;
                StartLongitude = point.Longitude;
                StartGeocoded = DateTime.UtcNow;
            }
            else if (Direction == Models.Drive.DirectionFromServiceProvider)
            {
                EndLatitude = point.Latitude;
                EndLongitude = point.Longitude;
                EndGeocoded = DateTime.UtcNow;
            }
            else
            {
                string errorMessage = $"Invalid value for {nameof(Drive)}.{nameof(Direction)}: {Direction}";
                throw new InvalidOperationException(errorMessage);
            }
        }

    }
}
