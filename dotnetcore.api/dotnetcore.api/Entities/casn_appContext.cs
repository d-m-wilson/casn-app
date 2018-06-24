using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace dotnetcore.api.Entities
{
    public partial class casn_appContext : DbContext
    {
        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<Drive> Drive { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Volunteer> Volunteer { get; set; }
        public virtual DbSet<VolunteerDrive> VolunteerDrive { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql(Secrets.DBConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("appointment");

                entity.HasIndex(e => e.DispatcherId)
                    .HasName("fk_appointment_DispatcherId_idx");

                entity.HasIndex(e => e.PatientId)
                    .HasName("fk_appointment_PatientId_idx");

                entity.Property(e => e.AppointmentDate).HasColumnType("datetime");

                entity.Property(e => e.AppointmentTypeId).HasColumnType("int(10)");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    //.HasDefaultValueSql("'CURRENT_TIMESTAMP'");
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DropoffLocationVague).HasMaxLength(255);

                entity.Property(e => e.PickupLocationVague).HasMaxLength(255);

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnUpdate();

                entity.HasOne(d => d.Dispatcher)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.DispatcherId)
                    .HasConstraintName("fk_appointment_DispatcherId");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("fk_appointment_PatientId");
            });

            modelBuilder.Entity<Drive>(entity =>
            {
                entity.ToTable("drive");

                entity.HasIndex(e => e.AppointmentId)
                    .HasName("fk_drive_AppointmentId_idx");

                entity.HasIndex(e => e.DriverId)
                    .HasName("fk_drive_DriverId_idx");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    //.HasDefaultValueSql("'CURRENT_TIMESTAMP'");
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.EndAddress).HasMaxLength(100);

                entity.Property(e => e.EndCity).HasMaxLength(50);

                entity.Property(e => e.EndPostalCode).HasMaxLength(10);

                entity.Property(e => e.EndState).HasMaxLength(30);

                entity.Property(e => e.StartAddress).HasMaxLength(100);

                entity.Property(e => e.StartCity).HasMaxLength(50);

                entity.Property(e => e.StartPostalCode).HasMaxLength(10);

                entity.Property(e => e.StartState).HasMaxLength(30);

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnUpdate();

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

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    //.HasDefaultValueSql("'CURRENT_TIMESTAMP'");
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsMinor).HasColumnType("bit(1)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PatientIdentifier)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PreferredContactMethod).HasColumnType("tinyint(1)");

                entity.Property(e => e.PreferredLanguage)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnUpdate();
            });

            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.ToTable("volunteer");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    //.HasDefaultValueSql("'CURRENT_TIMESTAMP'");
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GoogleAccount)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");

                entity.Property(e => e.IsDispatcher).HasColumnType("bit(1)");

                entity.Property(e => e.IsDriver).HasColumnType("bit(1)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MobilePhone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnUpdate();
            });

            modelBuilder.Entity<VolunteerDrive>(entity =>
            {
                entity.ToTable("volunteer_drive");

                entity.HasIndex(e => e.DriveId)
                    .HasName("fk_volunteer_drive_DriveId_idx");

                entity.HasIndex(e => e.VolunteerId)
                    .HasName("fk_volunteer_drive_VolunteerId_idx");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    //.HasDefaultValueSql("'CURRENT_TIMESTAMP'");
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnUpdate();

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
