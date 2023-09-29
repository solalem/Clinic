using Solo.ViewModels;
using Solo.ViewModels.Appointments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solo.Appointments.Services
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
