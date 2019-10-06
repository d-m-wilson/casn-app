using System;

namespace CASNApp.Core.Models
{
    public partial class Drive
    {
        public const int DirectionToServiceProvider = 1;
        public const int DirectionFromServiceProvider = 2;
        public const int StatusOpen = 0;
        public const int StatusPending = 1;
        public const int StatusApproved = 2;
        public const int StatusCanceled = 3;

        public Drive() { }

        public Drive(Entities.Drive d)
        {
            AppointmentId = d.AppointmentId;
            Created = d.Created;
            Direction = d.Direction;
            StatusId = d.StatusId;
            Status = d.Status?.Name;
            DriverId = d.DriverId;
            EndAddress = d.EndAddress;
            EndCity = d.EndCity;
            EndPostalCode = d.EndPostalCode;
            EndState = d.EndState;
            EndLatitude = d.EndLatitude;
            EndLongitude = d.EndLongitude;
            Id = d.Id;
            StartAddress = d.StartAddress;
            StartCity = d.StartCity;
            StartPostalCode = d.StartPostalCode;
            StartState = d.StartState;
            StartLatitude = d.StartLatitude;
            StartLongitude = d.StartLongitude;
            Updated = d.Updated;
            Approved = d.Approved;
            ApprovedById = d.ApprovedById;
            CancelReasonId = d.CancelReasonId;
            CancelReason = d.CancelReason?.Name;
        }

        public bool Validate(int direction)
        {
            if (!Direction.HasValue)
                return false;

            if (direction == DirectionToServiceProvider &&
                Direction != DirectionToServiceProvider)
                return false;

            if (direction == DirectionFromServiceProvider &&
                Direction != DirectionFromServiceProvider)
                return false;

            if (Direction.Value == DirectionToServiceProvider)
            {
                if (string.IsNullOrWhiteSpace(StartAddress))
                    return false;

                if (string.IsNullOrWhiteSpace(StartCity))
                    return false;

                if (string.IsNullOrWhiteSpace(StartState))
                    return false;
            }
            else if (Direction.Value == DirectionFromServiceProvider)
            {
                if (string.IsNullOrWhiteSpace(EndAddress))
                    return false;

                if (string.IsNullOrWhiteSpace(EndCity))
                    return false;

                if (string.IsNullOrWhiteSpace(EndState))
                    return false;
            }
            else
            {
                return false;
            }

            return true;
        }

        public void SetCallerLocation(Queries.GeocoderQuery.LatLng point)
        {
            if (Direction == DirectionToServiceProvider)
            {
                StartLatitude = point.Latitude;
                StartLongitude = point.Longitude;
            }
            else if (Direction == DirectionFromServiceProvider)
            {
                EndLatitude = point.Latitude;
                EndLongitude = point.Longitude;
            }
            else
            {
                string errorMessage = $"Invalid value for {nameof(Drive)}.{nameof(Direction)}: {Direction}";
                throw new InvalidOperationException(errorMessage);
            }
        }

        public void Redact(Appointment appointment)
        {
            if (appointment == null)
            {
                throw new ArgumentNullException(nameof(appointment));
            }

            if (appointment.Id != AppointmentId)
            {
                throw new ArgumentException("Appointment ID mismatch", nameof(appointment));
            }

            if (Direction == DirectionToServiceProvider)
            {
                StartLatitude = appointment.PickupVagueLatitude;
                StartLongitude = appointment.PickupVagueLongitude;
                StartAddress = appointment.PickupLocationVague;
                StartCity = "";
                StartState = "";
                StartPostalCode = "";
            }
            else if (Direction == DirectionFromServiceProvider)
            {
                EndLatitude = appointment.DropoffVagueLatitude;
                EndLongitude = appointment.DropoffVagueLongitude;
                EndAddress = appointment.DropoffLocationVague;
                EndCity = "";
                EndState = "";
                EndPostalCode = "";
            }
            else
            {
                string errorMessage = $"Invalid value for {nameof(Drive)}.{nameof(Direction)}: {Direction}";
                throw new InvalidOperationException(errorMessage);
            }
        }

    }
}
