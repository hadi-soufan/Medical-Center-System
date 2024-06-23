using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    /// <summary>
    /// Represents the database context for interacting with the underlying database.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<AppUser>, IApplicationDbContext
    {
        public virtual DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Receptionist> Receptionists{ get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Accountant> Accountants { get; set; }
        public DbSet<PatientPhoto> PatientPhotos { get; set; }
        public DbSet<Center> Centers { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Room> Rooms { get; set; }


        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Patient>()
                .HasMany(e => e.Appointments)
                .WithOne(e => e.Patient)
                .HasForeignKey(e => e.PatientId)
                .IsRequired();

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.User)
                .WithOne()
                .HasForeignKey<Doctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Nurse>()
                .HasOne(d => d.User)
                .WithOne()
                .HasForeignKey<Nurse>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Accountant>()
              .HasOne(a => a.User)
              .WithOne()
              .HasForeignKey<Accountant>(a => a.UserId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Center>()
                .HasMany(c => c.Buildings)
                .WithOne(b => b.Center)
                .HasForeignKey(b => b.CenterId)
                .IsRequired();

            modelBuilder.Entity<Building>()
                .HasMany(b => b.Departments)
                .WithOne(d => d.Building)
                .HasForeignKey(d => d.BuildingId)
                .IsRequired();

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Floors)
                .WithOne(f => f.Department)
                .HasForeignKey(f => f.DepartmentId)
                .IsRequired();

            modelBuilder.Entity<Floor>()
                .HasMany(f => f.Rooms)
                .WithOne(r => r.Floor)
                .HasForeignKey(r => r.FloorId)
                .IsRequired();
        }


    }
}
