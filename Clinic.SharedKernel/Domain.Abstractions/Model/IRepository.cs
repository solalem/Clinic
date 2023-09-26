namespace Clinic.SharedKernel.Domain.Abstractions.Model
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
