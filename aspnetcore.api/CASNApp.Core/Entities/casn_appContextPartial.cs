using System;
using System.Threading;
using System.Threading.Tasks;
using CASNApp.Core.Interfaces;
using CASNApp.Core.Misc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace CASNApp.Core.Entities
{
    public partial class casn_appContext
    {
        public casn_appContext()
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public casn_appContext(DbContextOptions<casn_appContext> options, IConfiguration configuration)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;

            if (bool.Parse(configuration[Constants.DBUseManagedIdentity]))
            {
                var tenantId = configuration[Constants.DBManagedIdentityTenantId];

                if (string.IsNullOrWhiteSpace(tenantId))
                {
                    tenantId = null;
                }

                var sqlConnection = GetSqlConnection();

                sqlConnection.AccessToken = ManagedIdentity.GetAccessTokenAsync(Constants.AzureSqlDatabaseResource, null, tenantId).Result;
            }
        }

        public SqlConnection GetSqlConnection()
        {
            return (SqlConnection)Database.GetDbConnection();
        }

        public override int SaveChanges()
        {
            UpdateEntities();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateEntities()
        {
            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Modified && entry.Entity is IUpdatedDate updatedEntity)
                {
                    updatedEntity.Updated = DateTime.UtcNow;

                    var createdProperty = entry.Property(nameof(IUpdatedDate.Created));
                    if (createdProperty != null && createdProperty.IsModified)
                    {
                        createdProperty.CurrentValue = createdProperty.OriginalValue;
                        createdProperty.IsModified = false;
                    }
                }

                if (entry.State == EntityState.Modified && entry.Entity is ICreatedDate updatedEntity2 &&
                    updatedEntity2.Created == DateTime.MinValue)
                {
                    // Don't upate Created datetime if it's the .NET default as SQL Server won't take it
                    entry.Property(nameof(updatedEntity2.Created)).IsModified = false;
                }

                if (entry.State == EntityState.Added && entry.Entity is ICreatedDate createdEntity)
                {
                    createdEntity.Created = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Deleted && entry.Entity is ISoftDelete deletedEntity)
                {
                    deletedEntity.IsActive = false;
                    entry.State = EntityState.Modified;
                }
            }
        }

    }
}
