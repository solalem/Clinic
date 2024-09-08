using Clinic.Core.Appointments.Domain.Patients;
using Clinic.Shared.Model;

namespace Clinic.Core.Appointments.Persistence.Patients
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer as requisite for each Aggregate
    public interface IPatientRepository : IRepository<Patient>
    {
        Patient Add(Patient patient);

        void Update(Patient patient);

        void Delete(Guid id);

        Task<Patient> GetAsync(Guid id);

        Task<bool> Exists(string cardNumber);
    }
}
