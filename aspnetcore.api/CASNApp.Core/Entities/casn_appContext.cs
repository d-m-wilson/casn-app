using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<FundingOffer> FundingOffers { get; set; }
        public virtual DbSet<FundingOfferItem> FundingOfferItems { get; set; }
        public virtual DbSet<FundingOfferStatus> FundingOfferStatuses { get; set; }
        public virtual DbSet<FundingSource> FundingSources { get; set; }
        public virtual DbSet<FundingType> FundingTypes { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageErrorLog> MessageErrorLogs { get; set; }
        public virtual DbSet<MessageLog> MessageLogs { get; set; }
        public virtual DbSet<MessageType> MessageTypes { get; set; }
        public virtual DbSet<NullReason> NullReasons { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
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
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                entity.HasIndex(e => e.AppointmentTypeId)
                    .HasName("FK_Appointment_AppointmentTypeId_idx");

                entity.HasIndex(e => e.CallerId)
                    .HasName("FK_Appointment_CallerId_idx");

                entity.HasIndex(e => e.DispatcherId)
                    .HasName("FK_Appointment_DispatcherId_idx");

                entity.HasIndex(e => e.ServiceProviderId)
                    .HasName("FK_Appointment_ServiceProviderId_idx");

                entity.Property(e => e.AppointmentDate).HasColumnType("datetime");

                entity.Property(e => e.BroadcastMessageDate).HasColumnType("datetime");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.DropoffLocationVague).HasMaxLength(255);

                entity.Property(e => e.DropoffVagueGeocoded).HasColumnType("datetime");

                entity.Property(e => e.DropoffVagueLatitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.DropoffVagueLongitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.PickupLocationVague).HasMaxLength(255);

                entity.Property(e => e.PickupVagueGeocoded).HasColumnType("datetime");

                entity.Property(e => e.PickupVagueLatitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.PickupVagueLongitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Tier1MessageDate).HasColumnType("datetime");

                entity.Property(e => e.Tier2MessageDate).HasColumnType("datetime");

                entity.Property(e => e.Tier3MessageDate).HasColumnType("datetime");

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.HasOne(d => d.AppointmentType)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.AppointmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_AppointmentTypeId");

                entity.HasOne(d => d.Caller)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.CallerId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Appointment_CallerId");

                entity.HasOne(d => d.Dispatcher)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DispatcherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_DispatcherId");

                entity.HasOne(d => d.ServiceProvider)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.ServiceProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_ServiceProviderId");
            });

            modelBuilder.Entity<AppointmentType>(entity =>
            {
                entity.ToTable("AppointmentType");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_AppointmentType_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Updated).HasColumnType("datetime");
            });

            modelBuilder.Entity<Badge>(entity =>
            {
                entity.ToTable("Badge");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.MessageText).HasMaxLength(250);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProcedureName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Updated).HasColumnType("datetime");
            });

            modelBuilder.Entity<Caller>(entity =>
            {
                entity.ToTable("Caller");

                entity.Property(e => e.CallerIdentifier)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PreferredLanguage)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.ResidencePostalCode).HasMaxLength(10);

                entity.Property(e => e.Updated).HasColumnType("datetime");
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

                entity.Property(e => e.Approved).HasColumnType("datetime");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.EndAddress).HasMaxLength(100);

                entity.Property(e => e.EndCity).HasMaxLength(50);

                entity.Property(e => e.EndGeocoded).HasColumnType("datetime");

                entity.Property(e => e.EndLatitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.EndLongitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.EndPostalCode).HasMaxLength(10);

                entity.Property(e => e.EndState).HasMaxLength(30);

                entity.Property(e => e.StartAddress).HasMaxLength(100);

                entity.Property(e => e.StartCity).HasMaxLength(50);

                entity.Property(e => e.StartGeocoded).HasColumnType("datetime");

                entity.Property(e => e.StartLatitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.StartLongitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.StartPostalCode).HasMaxLength(10);

                entity.Property(e => e.StartState).HasMaxLength(30);

                entity.Property(e => e.Updated).HasColumnType("datetime");

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
                    .WithMany(p => p.Drives)
                    .HasForeignKey(d => d.CancelReasonId)
                    .HasConstraintName("FK_Drive_CancelReasonId");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Drives)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_Drive_DriverId");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Drives)
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

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Updated).HasColumnType("datetime");
            });

            modelBuilder.Entity<DriveLogStatus>(entity =>
            {
                entity.ToTable("DriveLogStatus");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_DriveLogStatus_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Updated).HasColumnType("datetime");
            });

            modelBuilder.Entity<DriveStatus>(entity =>
            {
                entity.ToTable("DriveStatus");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_DriveStatus_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Updated).HasColumnType("datetime");
            });

            modelBuilder.Entity<FundingOffer>(entity =>
            {
                entity.ToTable("FundingOffer");

                entity.Property(e => e.AppointmentDate).HasColumnType("datetime");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Issued).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Paid).HasColumnType("datetime");

                entity.Property(e => e.Redeemed).HasColumnType("datetime");

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.Voided).HasColumnType("datetime");

                entity.HasOne(d => d.AppointmentType)
                    .WithMany(p => p.FundingOffers)
                    .HasForeignKey(d => d.AppointmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FundingOffer_AppointmentType");

                entity.HasOne(d => d.Caller)
                    .WithMany(p => p.FundingOffers)
                    .HasForeignKey(d => d.CallerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FundingOffer_Caller");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.FundingOffers)
                    .HasForeignKey(d => d.ClinicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FundingOffer_ServiceProvider");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.FundingOfferCreatedBies)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FundingOffer_Created_Volunteer");

                entity.HasOne(d => d.FundingOfferStatus)
                    .WithMany(p => p.FundingOffers)
                    .HasForeignKey(d => d.FundingOfferStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FundingOffer_FundingOfferStatus");

                entity.HasOne(d => d.IssuedBy)
                    .WithMany(p => p.FundingOfferIssuedBies)
                    .HasForeignKey(d => d.IssuedById)
                    .HasConstraintName("FK_FundingOffer_Issued_Volunteer");

                entity.HasOne(d => d.UpdatedBy)
                    .WithMany(p => p.FundingOfferUpdatedBies)
                    .HasForeignKey(d => d.UpdatedById)
                    .HasConstraintName("FK_FundingOffer_Updated_Volunteer");

                entity.HasOne(d => d.VoidedBy)
                    .WithMany(p => p.FundingOfferVoidedBies)
                    .HasForeignKey(d => d.VoidedById)
                    .HasConstraintName("FK_FundingOffer_Voided_Volunteer");
            });

            modelBuilder.Entity<FundingOfferItem>(entity =>
            {
                entity.ToTable("FundingOfferItem");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.FundingAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.NeedAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.HasOne(d => d.FundingAmountNullReason)
                    .WithMany(p => p.FundingOfferItemFundingAmountNullReasons)
                    .HasForeignKey(d => d.FundingAmountNullReasonId)
                    .HasConstraintName("FK_FundingOfferItem_FundingAmountNullReason");

                entity.HasOne(d => d.FundingOffer)
                    .WithMany(p => p.FundingOfferItems)
                    .HasForeignKey(d => d.FundingOfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FundingOfferItem_FundingOffer");

                entity.HasOne(d => d.FundingSource)
                    .WithMany(p => p.FundingOfferItems)
                    .HasForeignKey(d => d.FundingSourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FundingOfferItem_FundingSource");

                entity.HasOne(d => d.FundingType)
                    .WithMany(p => p.FundingOfferItems)
                    .HasForeignKey(d => d.FundingTypeId)
                    .HasConstraintName("FK_FundingOfferItem_FundingType");

                entity.HasOne(d => d.NeedAmountNullReason)
                    .WithMany(p => p.FundingOfferItemNeedAmountNullReasons)
                    .HasForeignKey(d => d.NeedAmountNullReasonId)
                    .HasConstraintName("FK_FundingOfferItem_NeedAmountNullReason");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.FundingOfferItems)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .HasConstraintName("FK_FundingOfferItem_PaymentMethod");
            });

            modelBuilder.Entity<FundingOfferStatus>(entity =>
            {
                entity.ToTable("FundingOfferStatus");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Updated).HasColumnType("datetime");
            });

            modelBuilder.Entity<FundingSource>(entity =>
            {
                entity.ToTable("FundingSource");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Updated).HasColumnType("datetime");
            });

            modelBuilder.Entity<FundingType>(entity =>
            {
                entity.ToTable("FundingType");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Updated).HasColumnType("datetime");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.HasIndex(e => e.MessageTypeId)
                    .HasName("FK_Message_MessageTypeId_idx");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.MessageText)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.HasOne(d => d.MessageType)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.MessageTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_MessageTypeId");
            });

            modelBuilder.Entity<MessageErrorLog>(entity =>
            {
                entity.ToTable("MessageErrorLog");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.DateSent).HasColumnType("datetime");

                entity.Property(e => e.ErrorCode).HasMaxLength(20);

                entity.Property(e => e.ErrorMessage).HasMaxLength(1000);

                entity.Property(e => e.FromPhone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Subject).HasMaxLength(500);

                entity.Property(e => e.ToPhone)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<MessageLog>(entity =>
            {
                entity.ToTable("MessageLog");

                entity.HasIndex(e => e.DateSent)
                    .HasName("IX_MessageErrorLog_DateSent");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.DateSent).HasColumnType("datetime");

                entity.Property(e => e.FromPhone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Subject).HasMaxLength(500);

                entity.Property(e => e.ToPhone)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<MessageType>(entity =>
            {
                entity.ToTable("MessageType");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_MessageType_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Updated).HasColumnType("datetime");
            });

            modelBuilder.Entity<NullReason>(entity =>
            {
                entity.ToTable("NullReason");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Updated).HasColumnType("datetime");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("PaymentMethod");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Updated).HasColumnType("datetime");
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

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Geocoded).HasColumnType("datetime");

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

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.HasOne(d => d.ServiceProviderType)
                    .WithMany(p => p.ServiceProviders)
                    .HasForeignKey(d => d.ServiceProviderTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceProvider_ServiceProviderTypeId");
            });

            modelBuilder.Entity<ServiceProviderType>(entity =>
            {
                entity.ToTable("ServiceProviderType");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_ServiceProviderType_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Updated).HasColumnType("datetime");
            });

            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.ToTable("Volunteer");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Geocoded).HasColumnType("datetime");

                entity.Property(e => e.GoogleAccount)
                    .IsRequired()
                    .HasMaxLength(100);

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

                entity.Property(e => e.Updated).HasColumnType("datetime");
            });

            modelBuilder.Entity<VolunteerBadge>(entity =>
            {
                entity.ToTable("Volunteer_Badge");

                entity.HasIndex(e => e.BadgeId)
                    .HasName("FK_Volunteer_Badge_BadgeId_idx");

                entity.HasIndex(e => e.VolunteerDriveLogId)
                    .HasName("FK_Volunteer_Badge_VolunteerDriveLogId_idx");

                entity.HasIndex(e => e.VolunteerId)
                    .HasName("FK_Volunteer_Badge_VolunteerId_idx");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.HasOne(d => d.Badge)
                    .WithMany(p => p.VolunteerBadges)
                    .HasForeignKey(d => d.BadgeId)
                    .HasConstraintName("FK_Volunteer_Badge_BadgeId");

                entity.HasOne(d => d.VolunteerDriveLog)
                    .WithMany(p => p.VolunteerBadges)
                    .HasForeignKey(d => d.VolunteerDriveLogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Volunteer_Badge_VolunteerDriveLogId");

                entity.HasOne(d => d.Volunteer)
                    .WithMany(p => p.VolunteerBadges)
                    .HasForeignKey(d => d.VolunteerId)
                    .HasConstraintName("FK_Volunteer_Badge_VolunteerId");
            });

            modelBuilder.Entity<VolunteerDriveLog>(entity =>
            {
                entity.ToTable("Volunteer_DriveLog");

                entity.HasIndex(e => e.DriveId)
                    .HasName("FK_Volunteer_Drive_DriveId_idx");

                entity.HasIndex(e => e.DriveLogStatusId)
                    .HasName("FK_Volunteer_Drive_DriveLogStatusId_idx");

                entity.HasIndex(e => e.VolunteerId)
                    .HasName("FK_Volunteer_Drive_VolunteerId_idx");

                entity.Property(e => e.Approved).HasColumnType("datetime");

                entity.Property(e => e.Canceled).HasColumnType("datetime");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Reassigned).HasColumnType("datetime");

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.HasOne(d => d.Drive)
                    .WithMany(p => p.VolunteerDriveLogs)
                    .HasForeignKey(d => d.DriveId)
                    .HasConstraintName("FK_Volunteer_DriveLog_DriveId");

                entity.HasOne(d => d.DriveLogStatus)
                    .WithMany(p => p.VolunteerDriveLogs)
                    .HasForeignKey(d => d.DriveLogStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Volunteer_DriveLog_DriveLogStatusId");

                entity.HasOne(d => d.Volunteer)
                    .WithMany(p => p.VolunteerDriveLogs)
                    .HasForeignKey(d => d.VolunteerId)
                    .HasConstraintName("FK_Volunteer_DriveLog_VolunteerId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
