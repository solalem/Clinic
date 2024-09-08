using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MediatR;
using Clinic.Core.Appointments.Domain.Patients;
using Clinic.Core.Appointments.Persistence.Patients;
using Clinic.Shared.Domain.Abstractions.Model;
using Clinic.Core.Appointments.Domain.Visits;
using Clinic.ViewModels.Appointments.Patients;

namespace Clinic.Core.Appointments.Persistence
{
    public class AppointmentsDbContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "Appointments";

        //public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visit> Visits { get; set; }

        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction => _currentTransaction;

        public AppointmentsDbContext(DbContextOptions<AppointmentsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: This is to suppress complaining about DomainEvents in Entities during creating migrations 
            modelBuilder.Ignore<Shared.Domain.Abstractions.Events.DomainEvent>();

            //modelBuilder.ApplyConfiguration(new AppointmentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PatientEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VisitEntityTypeConfiguration());
        }

        public async Task<int> SaveEntitiesAsync(string userId = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            AddAuditInfo(userId);
            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            AddAuditInfo(userId);
            return await base.SaveChangesAsync(cancellationToken);
        }

        public int SaveChanges(string userId = null)
        {
            AddAuditInfo(userId);
            return base.SaveChanges();
        }

        private void AddAuditInfo(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return;

            //// TODO: Add auditing information; createdBy/On, updatedBy/On
            //// get entries that are being Added or Updated
            //var modifiedEntries = ChangeTracker.Entries()
            //        .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));
            //foreach (var entry in modifiedEntries)
            //{
            //    if (!(entry.Entity is AuditableEntity entity)) continue;

            //    if (entry.State == EntityState.Added)
            //    {
            //        entity.CreatedBy = userId ?? "unknown";
            //        entity.CreatedDate = DateTime.Now;
            //    }
            //    entity.UpdatedBy = userId ?? "unknown";
            //    entity.UpdatedDate = DateTime.Now;
            //}
        }

        public async Task BeginTransactionAsync()
        {
            _currentTransaction = _currentTransaction ?? await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }

            var startUnderscores = System.Text.RegularExpressions.Regex.Match(input, @"^_+");
            return startUnderscores + System.Text.RegularExpressions.Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }
    }

}
