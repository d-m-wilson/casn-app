using CASNApp.Core;
using CASNApp.Core.Misc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CASNApp.Admin.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            if (bool.Parse(configuration[Constants.DBUseManagedIdentity]))
            {
                var tenantId = configuration[Constants.DBManagedIdentityTenantId];

                if (string.IsNullOrWhiteSpace(tenantId))
                {
                    tenantId = null;
                }

                var sqlConnection = (SqlConnection)Database.GetDbConnection();

                sqlConnection.AccessToken = ManagedIdentity.GetAccessTokenAsync(Constants.AzureSqlDatabaseResource, null, tenantId).Result;
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("admin");
        }

    }
}
