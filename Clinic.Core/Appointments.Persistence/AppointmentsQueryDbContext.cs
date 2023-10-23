using Microsoft.EntityFrameworkCore;
using Clinic.ViewModels.Appointments.Patients;
using Clinic.ViewModels.Appointments.Visits;

namespace Clinic.Core.Appointments.Persistence
{
    public class AppointmentsQueryDbContext : DbContext
    {
        public DbSet<PatientSummary> PatientSummaries { get; set; }
        public DbSet<VisitSummary> VisitSummaries { get; set; }

        public AppointmentsQueryDbContext(DbContextOptions<AppointmentsQueryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientSummary>().HasNoKey().ToTable((string?)null);
            modelBuilder.Entity<VisitSummary>().HasNoKey().ToTable((string?)null);
        }
    }
}
