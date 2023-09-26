using System;
using System.Threading.Tasks;
using Clinic.Domain.Abstractions.Model;
using Clinic.Core.Appointments.Domain.AggregatesModel;

namespace Clinic.Core.Appointments.Domain.Patients
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer as requisite for each Aggregate
    public interface IPatientRepository : IRepository<Patient>
    {
        Patient Add(Patient patient);
        
        void Update(Patient patient);
        
        void Delete(Guid id);

        Task<Patient> GetAsync(Guid id);
    }
}
