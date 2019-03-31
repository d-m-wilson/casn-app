using System;

namespace CASNApp.API.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime? SpecifyKind(this DateTime? dateTime, DateTimeKind dateTimeKind)
        {
            if (dateTime.HasValue && dateTime.Value.Kind != dateTimeKind)
            {
                return DateTime.SpecifyKind(dateTime.Value, dateTimeKind);
            }

            return dateTime;
        }

        public static DateTime SpecifyKind(this DateTime dateTime, DateTimeKind dateTimeKind)
        {
            if (dateTime.Kind != dateTimeKind)
            {
                return DateTime.SpecifyKind(dateTime, dateTimeKind);
            }

            return dateTime;
        }

    }
}
