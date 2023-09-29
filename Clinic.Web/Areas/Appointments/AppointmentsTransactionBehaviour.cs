using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Clinic.Core.Appointments.Persistence;

namespace Clinic.Web.Areas.Appointments
{
    public class AppointmentsTransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<AppointmentsTransactionBehaviour<TRequest, TResponse>> _logger;
        private readonly AppointmentsDbContext _dbContext;

        public AppointmentsTransactionBehaviour(AppointmentsDbContext dbContext, ILogger<AppointmentsTransactionBehaviour<TRequest, TResponse>> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(AppointmentsDbContext));
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = default;

            try
            {
                var strategy = _dbContext.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    _logger.LogInformation($"Begin transaction {typeof(TRequest).Name}");

                    await _dbContext.BeginTransactionAsync();

                    response = await next();

                    await _dbContext.CommitTransactionAsync();

                    _logger.LogInformation($"Committed transaction {typeof(TRequest).Name}");
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error during transaction {ex.Message}");

                _logger.LogInformation($"Rollback transaction executing {typeof(TRequest).Name}");

                _dbContext.RollbackTransaction();
                throw;
            }
        }
    }
}
