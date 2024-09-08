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
        Task<GetVisitResponse> GetAsync(GetVisitRequest request);
        Task<GetVisitsResponse> ListAsync(GetVisitsRequest request);
        Task<CreateVisitResponse> CreateAsync(CreateVisitRequest request);
        Task<UpdateVisitResponse> UpdateAsync(UpdateVisitRequest request);
        Task<ArchiveVisitResponse> DeleteAsync(ArchiveVisitRequest request);
    }
}
