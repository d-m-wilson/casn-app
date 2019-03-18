namespace CASNApp.API.Extensions
{
    public static class StringExtensions
    {
        private const string nullDisplayString = "{NULL}";

        public static string ApplyLogFormat(this string s)
        {
            if (s == null)
            {
                return nullDisplayString;
            }

            return $"\"{s}\"";
        }

    }
}
