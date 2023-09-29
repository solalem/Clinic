using Clinic.ViewModels;
using Clinic.ViewModels.Appointments.Patients;

namespace Clinic.Web.Areas.Appointments.Patients
{
    public interface IPatientService
    {
        Task<PatientDetail> GetAsync(Guid id);
        Task<PatientList> ListAsync(PaginationInfo pagination);
        Task<PatientSummary> CreateAsync(CreatePatient request);
        Task<PatientSummary> UpdateAsync(UpdatePatient request);
        Task<PatientSummary> DeleteAsync(Guid id);


    }
}
