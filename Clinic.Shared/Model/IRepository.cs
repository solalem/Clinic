namespace Clinic.Shared.Model
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
