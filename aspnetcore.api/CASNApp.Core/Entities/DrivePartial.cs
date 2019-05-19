using System;

namespace CASNApp.Core.Entities
{
    public partial class Drive
    {
        public string GetCallerAddress()
        {
            if (Direction == Models.Drive.DirectionToClinic)
            {
                return $"{StartAddress}, {StartCity}, {StartState} {StartPostalCode}";
            }
            else if (Direction == Models.Drive.DirectionFromClinic)
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
            if (Direction == Models.Drive.DirectionToClinic)
            {
                StartLatitude = point.Latitude;
                StartLongitude = point.Longitude;
                StartGeocoded = DateTime.UtcNow;
            }
            else if (Direction == Models.Drive.DirectionFromClinic)
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
