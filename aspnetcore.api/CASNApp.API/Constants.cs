namespace CASNApp.API
{
    internal static class Constants
    {
        internal const string JwtBearerAuthority = nameof(JwtBearerAuthority);
        internal const string JwtBearerAudience = nameof(JwtBearerAudience);
        internal const bool JwtBearerRequireHttpsMetadata = true;
        internal const string JwtBearerRoleClaimType = "http://example.com/groups";
        internal const string JwtBearerEmailClaimType = "http://example.com/email";

    }
}
