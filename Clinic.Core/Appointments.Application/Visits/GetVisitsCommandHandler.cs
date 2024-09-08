using Clinic.Core.Appointments.Domain.Visits;
using Clinic.Shared;
using Clinic.ViewModels.Appointments.Visits;
using MediatR;

namespace Clinic.Core.Appointments.Application.Visits
{
    public class GetVisitsCommandHandler
        : IRequestHandler<GetVisitsCommand, GetVisitsResponse>
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IIdentityService _identityService;

        public GetVisitsCommandHandler(
            IVisitRepository visitRepository,
            IIdentityService identityService)
        {
            _visitRepository = visitRepository ?? throw new ArgumentNullException(nameof(visitRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<GetVisitsResponse> Handle(GetVisitsCommand message, CancellationToken cancellationToken)
        {
            var pagination = message.Request.PaginationInfo;
            var models = await _visitRepository.GetManyAsync(message.Request.PaginationInfo);
            pagination.TotalItems = await _visitRepository.GetCountAsync(message.Request.PaginationInfo);

            return new GetVisitsResponse
            {
                Succeed = true,
                Data = new VisitList
                {
                    PaginationInfo = pagination,
                    Items = VisitMapper.FromModel(models).ToList()
                }
            };
        }
    }

    /// <summary>
    /// Visit Command Model
    /// </summary>
    public class GetVisitsCommand : IRequest<GetVisitsResponse>
    {
        public GetVisitsRequest Request { get; set; }

        public GetVisitsCommand(GetVisitsRequest request)
        {
            Request = request;
        }
    }
}
