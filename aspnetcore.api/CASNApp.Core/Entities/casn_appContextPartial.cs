using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CASNApp.Core.Interfaces;
using CASNApp.Core.Misc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            ApplyValueConversions(modelBuilder);

            modelBuilder.Entity<FundingOffer>()
                .Property(fo => fo.FundingOfferStatusId)
                .HasField("_FundingOfferStatusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

        }

        private void ApplyValueConversions(ModelBuilder modelBuilder)
        {
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(v => v, v => DateTime.SpecifyKind(v.Value, DateTimeKind.Utc));

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?));
                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof(DateTime))
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name)
                            .HasConversion(dateTimeConverter);
                    }
                    else if (property.PropertyType == typeof(DateTime?))
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name)
                            .HasConversion(nullableDateTimeConverter);
                    }
                }
            }
        }

    }
}
