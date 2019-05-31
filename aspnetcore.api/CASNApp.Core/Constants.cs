using System;

namespace CASNApp.Core
{
    public static class Constants
    {
        public const string DbConnectionString = nameof(DbConnectionString);
        public const string MySQLVersionString = nameof(MySQLVersionString);
        public const string MySQLRetryCount = nameof(MySQLRetryCount);
        public const string GoogleApiKey = nameof(GoogleApiKey);
        public const string VagueLocationMinOffset = nameof(VagueLocationMinOffset);
        public const string VagueLocationMaxOffset = nameof(VagueLocationMaxOffset);
        public const string NLogConfigFile = "nlog.config";
		public const string TwilioAccountSID = nameof(TwilioAccountSID);
		public const string TwilioAuthKey = nameof(TwilioAuthKey);
		public const string TwilioPhoneNumber = nameof(TwilioPhoneNumber);
	}
}
