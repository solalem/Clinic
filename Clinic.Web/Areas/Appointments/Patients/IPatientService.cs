using Clinic.ViewModels.Appointments.Patients;

namespace Clinic.Web.Areas.Appointments.Patients
{
    public interface IPatientService
    {
        Task<GetPatientResponse> GetAsync(GetPatientRequest request);
        Task<GetPatientsResponse> ListAsync(GetPatientsRequest request);
        Task<CreatePatientResponse> CreateAsync(CreatePatientRequest request);
        Task<UpdatePatientResponse> UpdateAsync(UpdatePatientRequest request);
        Task<ArchivePatientResponse> DeleteAsync(Guid id);

    }
}
