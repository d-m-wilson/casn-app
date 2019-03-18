using System;

namespace CASNApp.API
{
    internal static class Constants
    {
        // Actual constants
        internal const string DbConnectionString = nameof(DbConnectionString);
        internal const string GoogleApiKey = nameof(GoogleApiKey);
        internal const string VagueLocationMinOffset = nameof(VagueLocationMinOffset);
        internal const string VagueLocationMaxOffset = nameof(VagueLocationMaxOffset);
        internal const string NLogConfigFile = "nlog.config";
        internal const string JwtBearerAuthority = nameof(JwtBearerAuthority);
        internal const string JwtBearerAudience = nameof(JwtBearerAudience);
        internal const bool JwtBearerRequireHttpsMetadata = true;
        internal const string JwtBearerRoleClaimType = "http://example.com/groups";
        internal const string JwtBearerEmailClaimType = "http://example.com/email";
        internal const string DispatchersRoleName = nameof(DispatchersRoleName);
        internal const string DriversRoleName = nameof(DriversRoleName);
        internal const string IsDispatcherPolicy = nameof(IsDispatcherPolicy);
        internal const string IsDriverPolicy = nameof(IsDriverPolicy);
        internal const string IsDispatcherOrDriverPolicy = nameof(IsDispatcherOrDriverPolicy);

        // Sort-of-constants
        internal static readonly Version MySQLVersion = new Version(5, 7, 22);

    }
}
