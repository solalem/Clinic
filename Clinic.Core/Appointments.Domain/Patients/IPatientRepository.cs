using Clinic.Shared.Model;
using Clinic.Core.Appointments.Persistence.Patients;
using Clinic.ViewModels;

namespace Clinic.Core.Appointments.Domain.Patients
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Patient Add(Patient patient);
        
        void Update(Patient patient);
        
        void Delete(Guid id);

        Task<Patient> GetAsync(Guid id);

        Task<int> GetCountAsync(PaginationInfo paginationInfo);
        
        Task<IEnumerable<Patient>> GetManyAsync(PaginationInfo pagination);

    }
}
