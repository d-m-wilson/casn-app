﻿namespace CASNApp.API.Models
{
    public partial class Drive
    {
        public const int DirectionToClinic = 1;
        public const int DirectionFromClinic = 2;

        public Drive() { }

        public Drive(Entities.Drive d)
        {
            AppointmentId = d.AppointmentId;
            Created = d.Created;
            Direction = d.Direction;
            DriverId = d.DriverId;
            EndAddress = d.EndAddress;
            EndCity = d.EndCity;
            EndPostalCode = d.EndPostalCode;
            EndState = d.EndState;
            Id = d.Id;
            StartAddress = d.StartAddress;
            StartCity = d.StartCity;
            StartPostalCode = d.StartPostalCode;
            StartState = d.StartState;
            Updated = d.Updated;
        }

        public bool Validate(int direction)
        {
            if (!Direction.HasValue)
                return false;

            if (direction == DirectionToClinic &&
                Direction != DirectionToClinic)
                return false;

            if (direction == DirectionFromClinic &&
                Direction != DirectionFromClinic)
                return false;

            if (Direction.Value == DirectionToClinic)
            {
                if (string.IsNullOrWhiteSpace(StartAddress))
                    return false;

                if (string.IsNullOrWhiteSpace(StartCity))
                    return false;

                if (string.IsNullOrWhiteSpace(StartState))
                    return false;
            }
            else if (Direction.Value == DirectionFromClinic)
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

    }
}
