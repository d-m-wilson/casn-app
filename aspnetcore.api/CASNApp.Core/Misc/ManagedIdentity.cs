using System.Threading.Tasks;
using Microsoft.Azure.Services.AppAuthentication;

namespace CASNApp.Core.Misc
{
    public static class ManagedIdentity
    {

        public static async Task<string> GetAccessTokenAsync(string resource, string tokenProviderConnectionString, string tenantId)
        {
            var authProvider = new AzureServiceTokenProvider(tokenProviderConnectionString);

            if (string.IsNullOrWhiteSpace(tenantId))
            {
                tenantId = null;
            }

            return await authProvider.GetAccessTokenAsync(resource, tenantId);
        }
    }
}
