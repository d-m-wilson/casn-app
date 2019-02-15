using Microsoft.AspNetCore.Http;

namespace CASNApp.API.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserEmail(this HttpContext httpContext)
        {
            var emailClaim = httpContext.User.FindFirst(Constants.JwtBearerEmailClaimType);
            return emailClaim?.Value;
        }

    }
}
