using System;

namespace CASNApp.API
{
    internal static class Constants
    {
        // Actual constants
        internal const string DbConnectionString = nameof(DbConnectionString);
        internal const string JwtBearerAuthority = nameof(JwtBearerAuthority);
        internal const string JwtBearerAudience = nameof(JwtBearerAudience);
        internal const bool JwtBearerRequireHttpsMetadata = true;
        internal const string JwtBearerRoleClaimType = "http://example.com/groups";
        internal const string JwtBearerEmailClaimType = "http://example.com/email";

        internal static class Roles
        {
            internal const string Dispatchers = "dispatchers";
            internal const string Drivers = "drivers";
            internal const string Any = Dispatchers + "," + Drivers;
        }

        // Sort-of-constants
        internal static readonly Version MySQLVersion = new Version(5, 7, 22);

    }
}
