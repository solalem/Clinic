using Clinic.Core.Appointments.Domain.Visits;
using Clinic.Shared;
using Clinic.ViewModels.Appointments.Visits;
using MediatR;

namespace Clinic.Core.Appointments.Application.Visits
{
    public class GetVisitCommandHandler
        : IRequestHandler<GetVisitCommand, GetVisitResponse>
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IIdentityService _identityService;

        public GetVisitCommandHandler(
            IVisitRepository visitRepository, 
            IIdentityService identityService)
        {
            _visitRepository = visitRepository ?? throw new ArgumentNullException(nameof(visitRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<GetVisitResponse> Handle(GetVisitCommand message, CancellationToken cancellationToken)
        {
            var model = await _visitRepository.GetAsync(message.Request.Id);

            return new GetVisitResponse
            {
                Succeed = true,
                Data = VisitMapper.FromModel(model)
            };
        }
    }

    /// <summary>
    /// Visit Command Model
    /// </summary>
    public class GetVisitCommand : IRequest<GetVisitResponse>
    {
        public GetVisitRequest Request { get; set; }

        public GetVisitCommand(GetVisitRequest request)
        {
            Request = request;
        }
    }
}
