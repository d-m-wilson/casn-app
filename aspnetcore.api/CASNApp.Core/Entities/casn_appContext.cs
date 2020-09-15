using System;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Core.Entities
{
    public partial class casn_appContext : DbContext
    {

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<AppointmentType> AppointmentTypes { get; set; }
        public virtual DbSet<Badge> Badges { get; set; }
        public virtual DbSet<Caller> Callers { get; set; }
        public virtual DbSet<Drive> Drives { get; set; }
        public virtual DbSet<DriveCancelReason> DriveCancelReasons { get; set; }
        public virtual DbSet<DriveLogStatus> DriveLogStatuses { get; set; }
        public virtual DbSet<DriveStatus> DriveStatuses { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageErrorLog> MessageErrorLogs { get; set; }
        public virtual DbSet<MessageLog> MessageLogs { get; set; }
        public virtual DbSet<MessageType> MessageTypes { get; set; }
        public virtual DbSet<ServiceProvider> ServiceProviders { get; set; }
        public virtual DbSet<ServiceProviderType> ServiceProviderTypes { get; set; }
        public virtual DbSet<Volunteer> Volunteers { get; set; }
        public virtual DbSet<VolunteerBadge> VolunteerBadges { get; set; }
        public virtual DbSet<VolunteerDriveLog> VolunteerDriveLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                entity.HasIndex(e => e.AppointmentTypeId)
                    .HasName("FK_Appointment_AppointmentTypeId_idx");

                entity.HasIndex(e => e.CallerId)
                    .HasName("FK_Appointment_CallerId_idx");

                entity.HasIndex(e => e.ServiceProviderId)
                    .HasName("FK_Appointment_ServiceProviderId_idx");

                entity.HasIndex(e => e.DispatcherId)
                    .HasName("FK_Appointment_DispatcherId_idx");

                entity.Property(e => e.AppointmentDate)
                    .HasColumnType("datetime");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DropoffLocationVague).HasMaxLength(255);

                entity.Property(e => e.DropoffVagueGeocoded)
                    .HasColumnType("datetime");

                entity.Property(e => e.DropoffVagueLatitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.DropoffVagueLongitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(0x01)");

                entity.Property(e => e.PickupLocationVague).HasMaxLength(255);

                entity.Property(e => e.PickupVagueGeocoded)
                    .HasColumnType("datetime");

                entity.Property(e => e.PickupVagueLatitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.PickupVagueLongitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime");

                entity.HasOne(d => d.AppointmentType)
                    .WithMany()
                    .HasForeignKey(d => d.AppointmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_AppointmentTypeId");

                entity.HasOne(d => d.Caller)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.CallerId)
                    .HasConstraintName("FK_Appointment_CallerId");

                entity.HasOne(d => d.ServiceProvider)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.ServiceProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_ServiceProviderId");

                entity.HasOne(d => d.Dispatcher)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DispatcherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_DispatcherId");

				entity.Property(e => e.Tier1MessageCount).HasColumnType("integer");

                entity.Property(e => e.Tier1MessageDate)
                    .HasColumnType("datetime");

                entity.Property(e => e.Tier2MessageCount).HasColumnType("integer");

                entity.Property(e => e.Tier2MessageDate)
                    .HasColumnType("datetime");

                entity.Property(e => e.Tier3MessageCount).HasColumnType("integer");

                entity.Property(e => e.Tier3MessageDate)
                    .HasColumnType("datetime");

                entity.Property(e => e.BroadcastMessageCount).HasColumnType("integer");

                entity.Property(e => e.BroadcastMessageDate)
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<AppointmentType>(entity =>
            {
                entity.ToTable("AppointmentType");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_AppointmentType_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.EstimatedDurationMinutes).HasDefaultValueSql("((60))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(0x01)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Badge>(entity =>
            {
                entity.ToTable("Badge");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.MessageText)
                    .HasMaxLength(250);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(0x01)");

                entity.Property(e => e.IsHidden)
                    .IsRequired()
                    .HasDefaultValueSql("(0x00)");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime");

                entity.Property(e => e.TriggerType)
                    .IsRequired()
                    .HasColumnType("int");

                entity.Property(e => e.ProcedureName)
                    .IsRequired()
                    .HasColumnType("nvarchar")
                    .HasMaxLength(100);

                entity.Property(e => e.ServiceProviderId)
                    .HasColumnType("int");

                entity.Property(e => e.CountTarget)
                    .HasColumnType("int");
            });

            modelBuilder.Entity<Caller>(entity =>
            {
                entity.ToTable("Caller");

                entity.Property(e => e.CallerIdentifier)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(0x01)");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PreferredLanguage)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Drive>(entity =>
            {
                entity.ToTable("Drive");

                entity.HasIndex(e => e.AppointmentId)
                    .HasName("FK_Drive_AppointmentId_idx");

                entity.HasIndex(e => e.ApprovedById)
                    .HasName("FK_Drive_ApprovedById_idx");

                entity.HasIndex(e => e.CancelReasonId)
                    .HasName("FK_Drive_CancelReasonId_idx");

                entity.HasIndex(e => e.DriverId)
                    .HasName("FK_Drive_DriverId_idx");

                entity.HasIndex(e => e.StatusId)
                    .HasName("FK_Drive_StatusId_idx");

                entity.Property(e => e.Approved)
                    .HasColumnType("datetime");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.EndAddress).HasMaxLength(100);

                entity.Property(e => e.EndCity).HasMaxLength(50);

                entity.Property(e => e.EndGeocoded)
                    .HasColumnType("datetime");

                entity.Property(e => e.EndLatitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.EndLongitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.EndPostalCode).HasMaxLength(10);

                entity.Property(e => e.EndState).HasMaxLength(30);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(0x01)");

                entity.Property(e => e.StartAddress).HasMaxLength(100);

                entity.Property(e => e.StartCity).HasMaxLength(50);

                entity.Property(e => e.StartGeocoded)
                    .HasColumnType("datetime");

                entity.Property(e => e.StartLatitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.StartLongitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.StartPostalCode).HasMaxLength(10);

                entity.Property(e => e.StartState).HasMaxLength(30);

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Drives)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Drive_AppointmentId");

                entity.HasOne(d => d.Approver)
                    .WithMany(p => p.Approvals)
                    .HasForeignKey(d => d.ApprovedById)
                    .HasConstraintName("FK_Drive_ApprovedById");

                entity.HasOne(d => d.CancelReason)
                    .WithMany()
                    .HasForeignKey(d => d.CancelReasonId)
                    .HasConstraintName("FK_Drive_CancelReasonId");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Drives)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_Drive_DriverId");

                entity.HasOne(d => d.Status)
                    .WithMany()
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Drive_StatusId");
            });

            modelBuilder.Entity<DriveCancelReason>(entity =>
            {
                entity.ToTable("DriveCancelReason");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_DriveCancelReason_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(0x01)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<DriveStatus>(entity =>
            {
                entity.ToTable("DriveStatus");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_DriveStatus_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(0x01)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.HasIndex(e => e.MessageTypeId)
                    .HasName("FK_Message_MessageTypeId_idx");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MessageText)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime");

                entity.HasOne(d => d.MessageType)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.MessageTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Message_MessageTypeId");

            });

            modelBuilder.Entity<MessageErrorLog>(entity =>
            {
                entity.ToTable("MessageErrorLog");

                entity.HasIndex(e => e.DateSent);

                entity.Property(e => e.AppointmentId);

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.DateSent)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.FromPhone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Subject)
                    .HasMaxLength(500);

                entity.Property(e => e.ToPhone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.VolunteerId)
                    .IsRequired();

                entity.Property(e => e.ErrorCode)
                    .HasMaxLength(20);

                entity.Property(e => e.ErrorMessage)
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<MessageLog>(entity =>
            {
                entity.ToTable("MessageLog");

                entity.HasIndex(e => e.DateSent);

				entity.Property(e => e.AppointmentId);

				entity.Property(e => e.Body)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.DateSent)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.FromPhone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Subject)
					.HasMaxLength(500);

                entity.Property(e => e.ToPhone)
                    .IsRequired()
                    .HasMaxLength(20);

				entity.Property(e => e.VolunteerId)
					.IsRequired();
			});

            modelBuilder.Entity<MessageType>(entity =>
            {
                entity.ToTable("MessageType");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_MessageType_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(0x01)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime");

            });

            modelBuilder.Entity<ServiceProvider>(entity =>
            {
                entity.ToTable("ServiceProvider");

                entity.HasIndex(e => e.ServiceProviderTypeId)
                    .HasName("FK_ServiceProvider_ServiceProviderTypeId_idx");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Geocoded)
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(0x01)");

                entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime");

                entity.HasOne(d => d.ServiceProviderType)
                    .WithMany(p => p.ServiceProviders)
                    .HasForeignKey(d => d.ServiceProviderTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ServiceProvider_ServiceProviderTypeId");
            });

            modelBuilder.Entity<ServiceProviderType>(entity =>
            {
                entity.ToTable("ServiceProviderType");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_ServiceProviderType_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(0x01)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime");

            });

            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.ToTable("Volunteer");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Geocoded)
                    .HasColumnType("datetime");

                entity.Property(e => e.GoogleAccount)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.HasTextConsent)
                    .IsRequired()
                    .HasDefaultValueSql("(0x01)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(0x01)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.MobilePhone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.State).HasMaxLength(30);

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<VolunteerBadge>(entity =>
            {
                entity.ToTable("Volunteer_Badge");

                entity.HasIndex(e => e.BadgeId)
                    .HasName("FK_Volunteer_Badge_BadgeId_idx");

                entity.HasIndex(e => e.VolunteerId)
                    .HasName("FK_Volunteer_Badge_VolunteerId_idx");

                entity.HasIndex(e => e.VolunteerDriveLogId)
                    .HasName("FK_Volunteer_Badge_VolunteerDriveLogId_idx");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Badge)
                    .WithMany(p => p.VolunteerBadges)
                    .HasForeignKey(d => d.BadgeId)
                    .HasConstraintName("FK_Volunteer_Badge_BadgeId");

                entity.HasOne(d => d.Volunteer)
                    .WithMany(p => p.VolunteerBadges)
                    .HasForeignKey(d => d.VolunteerId)
                    .HasConstraintName("FK_Volunteer_Badge_VolunteerId");

                entity.HasOne(d => d.VolunteerDriveLog)
                    .WithMany(p => p.VolunteerBadges)
                    .HasForeignKey(d => d.VolunteerDriveLogId)
                    .HasConstraintName("FK_Volunteer_Badge_VolunteerDriveLogId");

            });

            modelBuilder.Entity<VolunteerDriveLog>(entity =>
            {
                entity.ToTable("Volunteer_DriveLog");

                entity.HasIndex(e => e.DriveId)
                    .HasName("FK_Volunteer_DriveLog_DriveId_idx");

                entity.HasIndex(e => e.VolunteerId)
                    .HasName("FK_Volunteer_DriveLog_VolunteerId_idx");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime");

                entity.Property(e => e.Canceled)
                    .HasColumnType("datetime");

                entity.Property(e => e.Approved)
                    .HasColumnType("datetime");

                entity.Property(e => e.Reassigned)
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Drive)
                    .WithMany(p => p.VolunteerDriveLogs)
                    .HasForeignKey(d => d.DriveId)
                    .HasConstraintName("FK_Volunteer_Drive_DriveId");

                entity.HasOne(d => d.Volunteer)
                    .WithMany(p => p.VolunteerDriveLogs)
                    .HasForeignKey(d => d.VolunteerId)
                    .HasConstraintName("FK_Volunteer_Drive_VolunteerId");
            });

            ApplyValueConversions(modelBuilder);
        }

    }
}
