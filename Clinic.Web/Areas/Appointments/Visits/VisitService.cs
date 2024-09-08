using Clinic.Core.Appointments.Application.Visits;
using Clinic.ViewModels.Appointments.Visits;
using MediatR;

namespace Clinic.Web.Areas.Appointments.Visits
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


        public async Task<CreateVisitResponse> CreateAsync(CreateVisitRequest request)
        {
            return await _mediator.Send(new CreateVisitCommand(request));
        }

        public async Task<UpdateVisitResponse> UpdateAsync(UpdateVisitRequest request)
        {
            return await _mediator.Send(new UpdateVisitCommand(request));
        }

        public async Task<ArchiveVisitResponse> DeleteAsync(ArchiveVisitRequest request)
        {
            return await _mediator.Send(new ArchiveVisitCommand(request));
        }

        public async Task<GetVisitResponse> GetAsync(GetVisitRequest request)
        {
            return await _mediator.Send(new GetVisitCommand(request));
        }
        public async Task<GetVisitsResponse> ListAsync(GetVisitsRequest request)
        {
            return await _mediator.Send(new GetVisitsCommand(request));
        }

    }
}
