using System;
using CASNApp.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CASNApp.Core.Entities
{
    public partial class casn_appContext : DbContext
    {
        public casn_appContext()
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public casn_appContext(DbContextOptions<casn_appContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<AppointmentType> AppointmentType { get; set; }
        public virtual DbSet<Badge> Badge { get; set; }
        public virtual DbSet<Caller> Caller { get; set; }
        public virtual DbSet<Clinic> Clinic { get; set; }
        public virtual DbSet<Drive> Drive { get; set; }
        public virtual DbSet<DriveCancelReason> DriveCancelReason { get; set; }
        public virtual DbSet<DriveStatus> DriveStatus { get; set; }
        public virtual DbSet<Message> Message { get; set; }
		public virtual DbSet<MessageLog> MessageLog { get; set; }
		public virtual DbSet<Volunteer> Volunteer { get; set; }
        public virtual DbSet<VolunteerBadge> VolunteerBadge { get; set; }
        public virtual DbSet<VolunteerDrive> VolunteerDrive { get; set; }

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
                entity.ToTable("appointment");

                entity.HasIndex(e => e.AppointmentTypeId)
                    .HasName("fk_appointment_appointmentTypeId_idx");

                entity.HasIndex(e => e.ClinicId)
                    .HasName("fk_appointment_clinicId_idx");

                entity.HasIndex(e => e.DispatcherId)
                    .HasName("fk_appointment_DispatcherId_idx");

                entity.HasIndex(e => e.CallerId)
                    .HasName("fk_appointment_CallerId_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseMySqlIdentityColumn();

                entity.Property(e => e.AppointmentDate)
                    .HasColumnName("appointmentDate")
                    .HasColumnType("datetime")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.Property(e => e.AppointmentTypeId)
                    .HasColumnName("appointmentTypeId")
                    .HasColumnType("int(10)");

                entity.Property(e => e.ClinicId).HasColumnName("clinicId");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.Property(e => e.DispatcherId).HasColumnName("dispatcherId");

                entity.Property(e => e.DropoffLocationVague)
                    .HasColumnName("dropoffLocationVague")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DropoffVagueLatitude)
                    .HasColumnName("dropoffVagueLatitude")
                    .HasColumnType("decimal(9,6)");

                entity.Property(e => e.DropoffVagueLongitude)
                    .HasColumnName("dropoffVagueLongitude")
                    .HasColumnType("decimal(9,6)");

                entity.Property(e => e.DropoffVagueGeocoded)
                    .HasColumnName("dropoffVagueGeocoded")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''")
                    .HasDefaultValue(true);

                entity.Property(e => e.CallerId).HasColumnName("callerId");

                entity.Property(e => e.PickupLocationVague)
                    .HasColumnName("pickupLocationVague")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PickupVagueLatitude)
                    .HasColumnName("pickupVagueLatitude")
                    .HasColumnType("decimal(9,6)");

                entity.Property(e => e.PickupVagueLongitude)
                    .HasColumnName("pickupVagueLongitude")
                    .HasColumnType("decimal(9,6)");

                entity.Property(e => e.PickupVagueGeocoded)
                    .HasColumnName("pickupVagueGeocoded")
                    .HasColumnType("datetime");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.HasOne(d => d.AppointmentType)
                    .WithMany()
                    .HasForeignKey(d => d.AppointmentTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_appointment_appointmentTypeId");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.ClinicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_appointment_clinicId");

                entity.HasOne(d => d.Dispatcher)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DispatcherId)
                    .HasConstraintName("fk_appointment_DispatcherId");

                entity.HasOne(d => d.Caller)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.CallerId)
                    .HasConstraintName("fk_appointment_CallerId");
            });

			modelBuilder.Entity<Message>(entity =>
			{
				entity.ToTable("message");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.UseMySqlIdentityColumn();

				entity.Property(e => e.MessageType)
					.IsRequired()
					.HasColumnName("messageType");

				entity.Property(e => e.MessageText)
					.IsRequired()
					.HasColumnName("messageText")
					.HasColumnType("varchar(250)");

				entity.Property(e => e.Created)
					.HasColumnName("created")
					.HasColumnType("datetime")
					.HasDefaultValueSql("'CURRENT_TIMESTAMP'")
					.HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

				entity.Property(e => e.Updated)
					.HasColumnName("updated")
					.HasColumnType("datetime")
					.HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

			});

			modelBuilder.Entity<MessageLog>(entity =>
			{
				entity.ToTable("messagelog");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.UseMySqlIdentityColumn();

				entity.Property(e => e.FromPhone)
					.IsRequired()
					.HasColumnName("fromPhone")
					.HasColumnType("varchar(20)");

				entity.Property(e => e.ToPhone)
					.IsRequired()
					.HasColumnName("toPhone")
					.HasColumnType("varchar(20)");

				entity.Property(e => e.Subject)
					.IsRequired()
					.HasColumnName("subject")
					.HasColumnType("varchar(500)");

				entity.Property(e => e.Body)
					.IsRequired()
					.HasColumnName("body")
					.HasColumnType("varchar(4000)");

				entity.Property(e => e.DateSent)
					.HasColumnName("dateSent")
					.HasColumnType("datetime")
					.HasDefaultValueSql("'CURRENT_TIMESTAMP'")
					.HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));
			});

			modelBuilder.Entity<AppointmentType>(entity =>
            {
                entity.ToTable("appointmenttype");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(64)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(64)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''")
                    .HasDefaultValue(true);

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

            });

            modelBuilder.Entity<Badge>(entity =>
            {
                entity.ToTable("badge");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseMySqlIdentityColumn();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnName("path")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''")
                    .HasDefaultValue(true);

                entity.Property(e => e.IsHidden)
                    .IsRequired()
                    .HasColumnName("isHidden")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''")
                    .HasDefaultValue(false);

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));
            });

            modelBuilder.Entity<Clinic>(entity =>
            {
                entity.ToTable("clinic");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseMySqlIdentityColumn();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CiviContactId).HasColumnName("civiContactId");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''")
                    .HasDefaultValue(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasColumnName("postalCode")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("decimal(9,6)");

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasColumnType("decimal(9,6)");

                entity.Property(e => e.Geocoded)
                    .HasColumnName("geocoded")
                    .HasColumnType("datetime");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));
            });

            modelBuilder.Entity<Drive>(entity =>
            {
                entity.ToTable("drive");

                entity.HasIndex(e => e.AppointmentId)
                    .HasName("fk_drive_AppointmentId_idx");

                entity.HasIndex(e => e.DriverId)
                    .HasName("fk_drive_DriverId_idx");

                entity.HasIndex(e => e.StatusId)
                    .HasName("fk_drive_statusId_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseMySqlIdentityColumn();

                entity.Property(e => e.AppointmentId)
                    .HasColumnName("appointmentId");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.Property(e => e.Direction)
                    .HasColumnName("direction");

                entity.Property(e => e.StatusId)
                    .HasColumnName("statusId");

                entity.Property(e => e.DriverId).HasColumnName("driverId");

                entity.Property(e => e.EndAddress)
                    .HasColumnName("endAddress")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.EndCity)
                    .HasColumnName("endCity")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.EndPostalCode)
                    .HasColumnName("endPostalCode")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.EndState)
                    .HasColumnName("endState")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.EndLatitude)
                    .HasColumnName("endLatitude")
                    .HasColumnType("decimal(9,6)");

                entity.Property(e => e.EndLongitude)
                    .HasColumnName("endLongitude")
                    .HasColumnType("decimal(9,6)");

                entity.Property(e => e.EndGeocoded)
                    .HasColumnName("endGeocoded")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''")
                    .HasDefaultValue(true);

                entity.Property(e => e.StartAddress)
                    .HasColumnName("startAddress")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.StartCity)
                    .HasColumnName("startCity")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.StartPostalCode)
                    .HasColumnName("startPostalCode")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.StartState)
                    .HasColumnName("startState")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.StartLatitude)
                    .HasColumnName("startLatitude")
                    .HasColumnType("decimal(9,6)");

                entity.Property(e => e.StartLongitude)
                    .HasColumnName("startLongitude")
                    .HasColumnType("decimal(9,6)");

                entity.Property(e => e.StartGeocoded)
                    .HasColumnName("startGeocoded")
                    .HasColumnType("datetime");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.Property(e => e.Approved)
                    .HasColumnName("approved")
                    .HasColumnType("datetime")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.Property(e => e.ApprovedById)
                    .HasColumnName("approvedById");

                entity.HasIndex(e => e.ApprovedById)
                    .HasName("fk_drive_ApprovedById_idx");

                entity.Property(e => e.CancelReasonId)
                    .HasColumnName("cancelReasonId");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Drives)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("fk_drive_AppointmentId");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Drives)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_drive_DriverId");

                entity.HasOne(d => d.Approver)
                    .WithMany(p => p.Approvals)
                    .HasForeignKey(d => d.ApprovedById)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_drive_ApprovedBy");

                entity.HasOne(d => d.CancelReason)
                    .WithMany()
                    .HasForeignKey(d => d.CancelReasonId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_drive_cancelReasonId");

                entity.HasOne(d => d.Status)
                    .WithMany()
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_drive_statusId");
            });

            modelBuilder.Entity<DriveCancelReason>(entity =>
            {
                entity.ToTable("drivecancelreason");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''")
                    .HasDefaultValue(true);

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

            });

            modelBuilder.Entity<DriveStatus>(entity =>
            {
                entity.ToTable("drivestatus");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''")
                    .HasDefaultValue(true);

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

            });

            modelBuilder.Entity<Caller>(entity =>
            {
                entity.ToTable("caller");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseMySqlIdentityColumn();

                entity.Property(e => e.CiviContactId).HasColumnName("civiContactId");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''")
                    .HasDefaultValue(true);

                entity.Property(e => e.IsMinor)
                    .HasColumnName("isMinor")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CallerIdentifier)
                    .IsRequired()
                    .HasColumnName("callerIdentifier")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.PreferredContactMethod)
                    .HasColumnName("preferredContactMethod")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.PreferredLanguage)
                    .IsRequired()
                    .HasColumnName("preferredLanguage")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));
            });

            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.ToTable("volunteer");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseMySqlIdentityColumn();

                entity.Property(e => e.CiviContactId)
                    .HasColumnName("civiContactId");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.MobilePhone)
                    .IsRequired()
                    .HasColumnName("mobilePhone")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.GoogleAccount)
                    .IsRequired()
                    .HasColumnName("googleAccount")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.IsDriver)
                    .HasColumnName("isDriver")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsDispatcher)
                    .HasColumnName("isDispatcher")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.HasTextConsent)
                    .HasColumnName("hasTextConsent")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''")
                    .HasDefaultValue(true);

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.PostalCode)
                    .HasColumnName("postalCode")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Latitude)
                    .HasColumnName("Latitude")
                    .HasColumnType("decimal(9,6)");

                entity.Property(e => e.Longitude)
                    .HasColumnName("Longitude")
                    .HasColumnType("decimal(9,6)");

                entity.Property(e => e.Geocoded)
                    .HasColumnName("geocoded")
                    .HasColumnType("date")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.Property(e => e.LastDriveDate)
                    .HasColumnName("lastDriveDate")
                    .HasColumnType("date")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''")
                    .HasDefaultValue(true);

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));
            });

            modelBuilder.Entity<VolunteerBadge>(entity =>
            {
                entity.ToTable("volunteer_badge");

                entity.HasIndex(e => e.BadgeId)
                    .HasName("fk_volunteer_badge_badgeId_idx");

                entity.HasIndex(e => e.VolunteerId)
                    .HasName("fk_volunteer_badge_volunteerId_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseMySqlIdentityColumn();

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.Property(e => e.BadgeId).HasColumnName("badgeId");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.Property(e => e.VolunteerId).HasColumnName("volunteerId");

                entity.HasOne(d => d.Badge)
                    .WithMany(p => p.VolunteerBadges)
                    .HasForeignKey(d => d.BadgeId)
                    .HasConstraintName("fk_volunteer_badge_badgeId");

                entity.HasOne(d => d.Volunteer)
                    .WithMany(p => p.VolunteerBadges)
                    .HasForeignKey(d => d.VolunteerId)
                    .HasConstraintName("fk_volunteer_badge_volunteerId");
            });

            modelBuilder.Entity<VolunteerDrive>(entity =>
            {
                entity.ToTable("volunteer_drive");

                entity.HasIndex(e => e.DriveId)
                    .HasName("fk_volunteer_drive_DriveId_idx");

                entity.HasIndex(e => e.VolunteerId)
                    .HasName("fk_volunteer_drive_VolunteerId_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseMySqlIdentityColumn();

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.Property(e => e.DriveId).HasColumnName("driveId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''")
                    .HasDefaultValue(true);

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasConversion(v => v, v => v.SpecifyKind(DateTimeKind.Utc));

                entity.Property(e => e.VolunteerId).HasColumnName("volunteerId");

                entity.HasOne(d => d.Drive)
                    .WithMany(p => p.VolunteerDrives)
                    .HasForeignKey(d => d.DriveId)
                    .HasConstraintName("fk_volunteer_drive_DriveId");

                entity.HasOne(d => d.Volunteer)
                    .WithMany(p => p.VolunteerDrives)
                    .HasForeignKey(d => d.VolunteerId)
                    .HasConstraintName("fk_volunteer_drive_VolunteerId");
            });
        }

    }
}
