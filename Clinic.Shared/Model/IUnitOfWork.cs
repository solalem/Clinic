namespace Clinic.Shared.Model
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(string userid = null, CancellationToken cancellationToken = default);
        Task<int> SaveEntitiesAsync(string userid = null, CancellationToken cancellationToken = default);
    }
}
