using System;

namespace CASNApp.Core
{
    public static class Constants
    {
        public const string DbConnectionString = nameof(DbConnectionString);
        public const string DBRetryCount = nameof(DBRetryCount);
        public const string DBUseManagedIdentity = nameof(DBUseManagedIdentity);
        public const string AzureSqlDatabaseResource = "https://database.windows.net/";
        public const string DBManagedIdentityTenantId = nameof(DBManagedIdentityTenantId);
        public const string GoogleApiKey = nameof(GoogleApiKey);
        public const string VagueLocationMinOffset = nameof(VagueLocationMinOffset);
        public const string VagueLocationMaxOffset = nameof(VagueLocationMaxOffset);
        public const string NLogConfigFile = "nlog.config";
        public const string TwilioIsEnabled = nameof(TwilioIsEnabled);
        public const string TwilioAccountSID = nameof(TwilioAccountSID);
		public const string TwilioAuthKey = nameof(TwilioAuthKey);
		public const string TwilioPhoneNumber = nameof(TwilioPhoneNumber);
        public const string BadgesAreEnabled = nameof(BadgesAreEnabled);
        public const string UserTimeZoneName = nameof(UserTimeZoneName);
        public const string CASNAppURL = nameof(CASNAppURL);
        public const string WEBJOBS_APPINSIGHTS_INSTRUMENTATIONKEY = nameof(WEBJOBS_APPINSIGHTS_INSTRUMENTATIONKEY);
        public const string ResponseCode_Success = "success";
        public const string ResponseCode_Error = "error";
    }
}
