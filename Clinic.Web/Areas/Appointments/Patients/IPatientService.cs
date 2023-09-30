using Clinic.ViewModels;
using Clinic.ViewModels.Appointments.Patients;

namespace Clinic.Web.Areas.Appointments.Patients
{
    public interface IPatientService
    {
        Task<PatientDetail> GetAsync(GetPatient request);
        Task<PatientList> ListAsync(GetPatients request);
        Task<PatientSummary> CreateAsync(CreatePatient request);
        Task<PatientSummary> UpdateAsync(UpdatePatient request);
        Task<int> DeleteAsync(Guid id);


    }
}
