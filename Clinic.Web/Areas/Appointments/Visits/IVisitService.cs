using Clinic.ViewModels;
using Clinic.ViewModels.Appointments;
using Clinic.ViewModels.Appointments.Visits;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Core.Appointments.Services
{
    public interface IVisitService
    {
        Task<VisitDetail> GetAsync(Guid id);
        Task<VisitList> ListAsync(PaginationInfo pagination);
        Task<VisitSummary> CreateAsync(CreateVisit request);
        Task<VisitSummary> UpdateAsync(UpdateVisit request);
        Task<VisitSummary> DeleteAsync(Guid id);


        Task<VisitList> ListVisitsByPatientIdAsync(Guid patientId, PaginationInfo pagination);
    }
}
