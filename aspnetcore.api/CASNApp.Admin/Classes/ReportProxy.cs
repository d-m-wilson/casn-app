using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ProxyKit;

namespace CASNApp.Admin
{
    public static class ReportProxy
    {
        public const string ReportProxyEnabled = nameof(ReportProxyEnabled);
        private const string ReportProxyPaths = nameof(ReportProxyPaths);
        private const string ReportServerUrl = nameof(ReportServerUrl);
        private const string ReportServerUsername = nameof(ReportServerUsername);
        private const string ReportServerPassword = nameof(ReportServerPassword);

        private static string reportServerUrl;
        private static string[] proxyPaths;
        private static AuthenticationHeaderValue reportServerAuthHeaderValue;

        public static IApplicationBuilder UseReportProxy(this IApplicationBuilder applicationBuilder, IConfiguration configuration)
        {
            Initialize(configuration);

            applicationBuilder.UseWhen(
                context => ShouldProxy(context),
                appInner => appInner.RunProxy(DoProxy)
            );

            return applicationBuilder;
        }

        private static void Initialize(IConfiguration configuration)
        {
            reportServerUrl = configuration[ReportServerUrl];

            if (string.IsNullOrWhiteSpace(reportServerUrl))
            {
                throw new Exception($"{nameof(ReportServerUrl)} is not configured correctly.");
            }

            if (!Uri.IsWellFormedUriString(reportServerUrl, UriKind.Absolute))
            {
                throw new Exception($"{nameof(ReportServerUrl)} is not valid.");
            }

            string proxyPathsCombined = configuration[ReportProxyPaths];

            if (string.IsNullOrWhiteSpace(proxyPathsCombined))
            {
                throw new Exception($"{nameof(ReportProxyPaths)} is not configured correctly.");
            }

            proxyPaths = proxyPathsCombined.Split(',');

            if (!string.IsNullOrWhiteSpace(configuration[ReportServerUsername]))
            {
                reportServerAuthHeaderValue = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{configuration[ReportServerUsername]}:{configuration[ReportServerPassword]}")));
            }

        }

        private static bool ShouldProxy(HttpContext context)
        {
            if (context.User != null &&
                context.User.Identity != null &&
                context.User.Identity.IsAuthenticated &&
                context.Request.Path.HasValue)
            {
                foreach (var proxyPath in proxyPaths)
                {
                    if (context.Request.Path.Value.StartsWith(proxyPath, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static async Task<HttpResponseMessage> DoProxy(HttpContext context)
        {
            var forwardContext = context
                .ForwardTo(reportServerUrl)
                .AddXForwardedHeaders();

            if (reportServerAuthHeaderValue != null)
            {
                forwardContext.UpstreamRequest.Headers.Authorization = reportServerAuthHeaderValue;
            }

            var response = await forwardContext.Send();

            response.Headers.Remove("X-Frame-Options");

            return response;
        }

    }
}
