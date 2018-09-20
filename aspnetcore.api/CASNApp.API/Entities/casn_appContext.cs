using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CASNApp.API.Entities
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
        public virtual DbSet<Clinic> Clinic { get; set; }
        public virtual DbSet<Drive> Drive { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Volunteer> Volunteer { get; set; }
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

                entity.HasIndex(e => e.ClinicId)
                    .HasName("fk_appointment_clinicId_idx");

                entity.HasIndex(e => e.DispatcherId)
                    .HasName("fk_appointment_DispatcherId_idx");

                entity.HasIndex(e => e.PatientId)
                    .HasName("fk_appointment_PatientId_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppointmentDate)
                    .HasColumnName("appointmentDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.AppointmentTypeId)
                    .HasColumnName("appointmentTypeId")
                    .HasColumnType("int(10)");

                entity.Property(e => e.ClinicId).HasColumnName("clinicId");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.DispatcherId).HasColumnName("dispatcherId");

                entity.Property(e => e.DropoffLocationVague)
                    .HasColumnName("dropoffLocationVague")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''")
                    .HasDefaultValue(true);

                entity.Property(e => e.PatientId).HasColumnName("patientId");

                entity.Property(e => e.PickupLocationVague)
                    .HasColumnName("pickupLocationVague")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.ClinicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_appointment_clinicId");

                entity.HasOne(d => d.Dispatcher)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.DispatcherId)
                    .HasConstraintName("fk_appointment_DispatcherId");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("fk_appointment_PatientId");
            });

            modelBuilder.Entity<Clinic>(entity =>
            {
                entity.ToTable("clinic");

                entity.Property(e => e.Id).HasColumnName("id");

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
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

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

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Drive>(entity =>
            {
                entity.ToTable("drive");

                entity.HasIndex(e => e.AppointmentId)
                    .HasName("fk_drive_AppointmentId_idx");

                entity.HasIndex(e => e.DriverId)
                    .HasName("fk_drive_DriverId_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppointmentId).HasColumnName("appointmentId");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Direction).HasColumnName("direction");

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

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Drive)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("fk_drive_AppointmentId");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Drive)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_drive_DriverId");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("patient");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CiviContactId).HasColumnName("civiContactId");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

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
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.PatientIdentifier)
                    .IsRequired()
                    .HasColumnName("patientIdentifier")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.PreferredContactMethod)
                    .HasColumnName("preferredContactMethod")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.PreferredLanguage)
                    .IsRequired()
                    .HasColumnName("preferredLanguage")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.ToTable("volunteer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CiviContactId).HasColumnName("civiContactId");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.GoogleAccount)
                    .IsRequired()
                    .HasColumnName("googleAccount")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.GoogleIdentityToken)
                    .HasColumnName("googleIdentityToken")
                    .HasColumnType("mediumtext");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''")
                    .HasDefaultValue(true);

                entity.Property(e => e.IsDispatcher)
                    .HasColumnName("isDispatcher")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsDriver)
                    .HasColumnName("isDriver")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.MobilePhone)
                    .IsRequired()
                    .HasColumnName("mobilePhone")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<VolunteerDrive>(entity =>
            {
                entity.ToTable("volunteer_drive");

                entity.HasIndex(e => e.DriveId)
                    .HasName("fk_volunteer_drive_DriveId_idx");

                entity.HasIndex(e => e.VolunteerId)
                    .HasName("fk_volunteer_drive_VolunteerId_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.DriveId).HasColumnName("driveId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''")
                    .HasDefaultValue(true);

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.VolunteerId).HasColumnName("volunteerId");

                entity.HasOne(d => d.Drive)
                    .WithMany(p => p.VolunteerDrive)
                    .HasForeignKey(d => d.DriveId)
                    .HasConstraintName("fk_volunteer_drive_DriveId");

                entity.HasOne(d => d.Volunteer)
                    .WithMany(p => p.VolunteerDrive)
                    .HasForeignKey(d => d.VolunteerId)
                    .HasConstraintName("fk_volunteer_drive_VolunteerId");
            });
        }

    }
}
