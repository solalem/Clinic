using System;
using System.Threading.Tasks;
using Clinic.SharedKernel.Domain.Abstractions.Model;

namespace Clinic.Core.Appointments.Domain.Patients
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer as requisite for each Aggregate
    public interface IPatientRepository : IRepository<Patient>
    {
        Patient Add(Patient patient);
        
        void Update(Patient patient);
        
        void Delete(Guid id);

        Task<Patient> GetAsync(Guid id);

        Task<IEnumerable<Patient>> GetManyAsync(int skip, int take, string? searchString = null);
    }
}
