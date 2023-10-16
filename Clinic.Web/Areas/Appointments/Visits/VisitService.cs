using Blazorise;
using Clinic.Core.Appointments.Application.Commands.ArchiveVisitCommands;
using Clinic.Core.Appointments.Application.Commands.CreateVisitCommands;
using Clinic.Core.Appointments.Application.Commands.UpdateVisitCommands;
using Clinic.Core.Appointments.Application.Patients;
using Clinic.Core.Appointments.Application.Visits;
using Clinic.ViewModels;
using Clinic.ViewModels.Appointments.Visits;
using Clinic.Web.Models;
using MediatR;

namespace Clinic.Core.Appointments.Services
{
    public class VisitService : IVisitService
    {
        private readonly IMediator _mediator;
        private readonly ILogger<VisitService> _logger;
        public VisitService(
            ILogger<VisitService> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        public async Task<VisitSummary> CreateAsync(CreateVisit request)
        {
            return await _mediator.Send(new CreateVisitCommand(request));
        }

        public async Task<VisitSummary> UpdateAsync(UpdateVisit request)
        {
            return await _mediator.Send(new UpdateVisitCommand(request));
        }

        public async Task<VisitSummary> DeleteAsync(Guid id)
        {
            return await _mediator.Send(new ArchiveVisitCommand(request));
        }

        public async Task<VisitDetail> GetAsync(Guid id)
        {
            return await _mediator.Send(new GetVisitCommand(new GetVisit { Id = id }));
        }

        public async Task<VisitList> ListAsync(PaginationInfo paginationInfo)
        {
            return await _mediator.Send(new GetVisitsCommand(new GetVisits { PaginationInfo = paginationInfo }));
        }

        public async Task<VisitList> ListVisitsByPatientIdAsync(Guid patientId, PaginationInfo paginationInfo)
        {
            return await _mediator.Send(new GetVisitsCommand(new GetVisits { PaginationInfo = paginationInfo }));
        }

    }
}
