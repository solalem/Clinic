using Clinic.Shared.Model;
using Clinic.ViewModels;

namespace Clinic.Core.Appointments.Domain.Visits
{
    public interface IVisitRepository : IRepository<Visit>
    {
        Visit Add(Visit visit);
        
        void Update(Visit visit);
        
        void Delete(Guid id);

        Task<Visit> GetAsync(Guid id);

        Task<int> GetCountAsync(PaginationInfo paginationInfo);
        
        Task<IEnumerable<Visit>> GetManyAsync(PaginationInfo pagination);

    }
}
