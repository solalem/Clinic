using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.SharedKernel.Domain.Abstractions.Model
{
    public interface IUnitOfWork : IDisposable
    {        
        Task<int> SaveChangesAsync(string userid = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveEntitiesAsync(string userid = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
