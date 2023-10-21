using Clinic.Core.Appointments.Domain.Visits;
using Clinic.SharedKernel.Domain.Abstractions.Model;
using Clinic.ViewModels;

namespace Clinic.Core.Appointments.Persistence.Visits
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer as requisite for each Aggregate
    public interface IVisitRepository : IRepository<Visit>
    {
        Visit Add(Visit visit);

        void Update(Visit visit);

        void Delete(Guid id);

        Task<Visit> GetAsync(Guid id);
        Task<IEnumerable<Visit>> GetManyAsync(PaginationInfo pagination);
    }
}
