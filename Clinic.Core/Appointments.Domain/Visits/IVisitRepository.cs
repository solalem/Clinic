using Clinic.Shared.Domain.Abstractions.Model;

namespace Clinic.Core.Appointments.Domain.Visits
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer as requisite for each Aggregate
    public interface IVisitRepository : IRepository<Visit>
    {
        Visit Add(Visit visit);

        void Update(Visit visit);

        void Delete(Guid id);

        Task<Visit> GetAsync(Guid id);
    }
}
