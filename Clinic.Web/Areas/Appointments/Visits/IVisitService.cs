using Clinic.ViewModels;
using Clinic.ViewModels.Appointments;
using Clinic.ViewModels.Appointments.Visits;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Web.Areas.Appointments.Visits
{
    public interface IVisitService
    {
        Task<VisitDetail> GetAsync(Guid id);
        Task<VisitList> ListAsync(PaginationInfo pagination);
        Task<VisitSummary> CreateAsync(CreateVisit request);
        Task<VisitSummary> UpdateAsync(UpdateVisit request);
        Task<int> DeleteAsync(Guid id);


        Task<VisitList> ListVisitsByPatientIdAsync(Guid patientId, PaginationInfo pagination);
    }
}
