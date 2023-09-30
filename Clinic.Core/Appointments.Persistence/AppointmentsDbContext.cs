using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using MediatR;
using Clinic.Core.Appointments.Domain.Patients;
using Clinic.Core.Appointments.Persistence.Patients;
using Clinic.SharedKernel.Domain.Abstractions.Model;

namespace Clinic.Core.Appointments.Persistence
{
    public class AppointmentsDbContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "Appointments";

        //public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        //public DbSet<Attendance> Attendances { get; set; }

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction => _currentTransaction;

        public AppointmentsDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: This is to suppress complaining about DomainEvents in Entities during creating migrations 
            modelBuilder.Ignore<SharedKernel.Domain.Abstractions.Events.DomainEvent>();

            //modelBuilder.ApplyConfiguration(new AppointmentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PatientEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new AttendanceEntityTypeConfiguration());

        }

        public async Task<int> SaveEntitiesAsync(string userId = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Pre Commit Domain Events collection. 
            //await _mediator.DispatchPreCommitDomainEventsAsync(this);

            AddAuditInfo(userId);
            var result = await base.SaveChangesAsync(cancellationToken);

            // Dispatch Post Commit Domain Events collection. 
            //await _mediator.DispatchPreCommitDomainEventsAsync(this);

            result += await base.SaveChangesAsync(cancellationToken);

            return result;
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
            if(string.IsNullOrEmpty(userId)) return;

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

    public class AppointmentsContextDesignFactory : IDesignTimeDbContextFactory<AppointmentsDbContext>
    {
        public AppointmentsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppointmentsDbContext>()
                .UseSqlite("data source=;Initial Catalog=Clinic;Integrated Security=true");
            return new AppointmentsDbContext(optionsBuilder.Options);
        }

        class NoMediator : IMediator
        {
            public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public IAsyncEnumerable<object> CreateStream(object request, CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public Task Publish(object notification, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.CompletedTask;
            }
            
            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }

            public Task<object> Send(object request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return null;// Task.CompletedTask;
            }

            public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
            {
                return null;
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
