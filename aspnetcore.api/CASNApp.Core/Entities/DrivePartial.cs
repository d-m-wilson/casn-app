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

        public static Drive CreateFromModel(Models.Drive driveModel, Appointment appointmentEntity, byte direction, ServiceProvider serviceProvider)
        {
            var drive = new Drive
            {
                Appointment = appointmentEntity,
                Direction = direction,
                DriverId = null,
                IsActive = true,
                Created = DateTime.UtcNow,
                Updated = null,
            };

            if (drive.Direction == Models.Drive.DirectionToServiceProvider)
            {
                drive.StartAddress = driveModel.StartAddress;
                drive.StartCity = driveModel.StartCity;
                drive.StartState = driveModel.StartState;
                drive.StartPostalCode = driveModel.StartPostalCode;
                drive.EndAddress = serviceProvider.Address;
                drive.EndCity = serviceProvider.City;
                drive.EndState = serviceProvider.State;
                drive.EndPostalCode = serviceProvider.PostalCode;
                drive.EndLatitude = serviceProvider.Latitude;
                drive.EndLongitude = serviceProvider.Longitude;
                drive.EndGeocoded = serviceProvider.Geocoded;
            }
            else if (drive.Direction == Models.Drive.DirectionFromServiceProvider)
            {
                drive.StartAddress = serviceProvider.Address;
                drive.StartCity = serviceProvider.City;
                drive.StartState = serviceProvider.State;
                drive.StartPostalCode = serviceProvider.PostalCode;
                drive.StartLatitude = serviceProvider.Latitude;
                drive.StartLongitude = serviceProvider.Longitude;
                drive.StartGeocoded = serviceProvider.Geocoded;
                drive.EndAddress = driveModel.EndAddress;
                drive.EndCity = driveModel.EndCity;
                drive.EndState = driveModel.EndState;
                drive.EndPostalCode = driveModel.EndPostalCode;
            }
            else
            {
                throw new InvalidOperationException($"Unsupported drive direction. (model.Direction={driveModel.Direction})");
            }

            return drive;
        }

        public void UpdateFromModel(Models.Drive driveModel)
        {
            if (Direction != (byte)driveModel.Direction.Value)
            {
                throw new InvalidOperationException($"Drive direction mismatch. (entity={Direction},model={driveModel.Direction})");
            }

            ApprovedById = (int?)driveModel.ApprovedById;
            CancelReasonId = driveModel.CancelReasonId;
            Updated = DateTime.UtcNow;
            
            if (Direction == Models.Drive.DirectionToServiceProvider)
            {
                StartAddress = driveModel.StartAddress;
                StartCity = driveModel.StartCity;
                StartState = driveModel.StartState;
                StartPostalCode = driveModel.StartPostalCode;
                StartGeocoded = null;
                StartLatitude = null;
                StartLongitude = null;
            }
            else if (Direction == Models.Drive.DirectionFromServiceProvider)
            {
                EndAddress = driveModel.EndAddress;
                EndCity = driveModel.EndCity;
                EndState = driveModel.EndState;
                EndPostalCode = driveModel.EndPostalCode;
                EndGeocoded = null;
                EndLatitude = null;
                EndLongitude = null;
            }
            else
            {
                throw new InvalidOperationException($"Unsupported drive direction. (entity.Direction={Direction})");
            }
        }

        /// <summary>
        /// Sanitizes/Redacts the Drive record prior to deletion. Necessary because we're doing soft deletes.
        /// </summary>
        public void Sanitize()
        {
            StartAddress = null;
            StartCity = null;
            StartState = null;
            StartLatitude = null;
            StartLongitude = null;
            StartGeocoded = null;
            EndAddress = null;
            EndCity = null;
            EndState = null;
            EndLatitude = null;
            EndLongitude = null;
            EndGeocoded = null;
        }

    }
}
