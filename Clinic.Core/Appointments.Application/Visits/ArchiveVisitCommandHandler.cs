using Clinic.Core.Appointments.Domain.Visits;
using Clinic.Shared;
using Clinic.ViewModels.Appointments.Visits;
using MediatR;

namespace Clinic.Core.Appointments.Application.Visits
{
    public class ArchiveVisitCommandHandler
        : IRequestHandler<ArchiveVisitCommand, ArchiveVisitResponse>
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IIdentityService _identityService;

        public ArchiveVisitCommandHandler(
            IVisitRepository visitRepository, 
            IIdentityService identityService)
        {
            _visitRepository = visitRepository ?? throw new ArgumentNullException(nameof(visitRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<ArchiveVisitResponse> Handle(ArchiveVisitCommand message, CancellationToken cancellationToken)
        {
            try
            {
                _visitRepository.Delete(message.Request.Id);
                await _visitRepository.UnitOfWork.SaveEntitiesAsync(_identityService.GetUserIdentity(), cancellationToken);
          
                return new ArchiveVisitResponse
                {
                    Succeed = true
                };
            }
            catch (Exception ex)
            {
                // TODO: log
                return new ArchiveVisitResponse { Error = ex.Message };
            }
        }
    }

    /// <summary>
    /// Visit Command Model
    /// </summary>
    public class ArchiveVisitCommand : IRequest<ArchiveVisitResponse>
    {
        public ArchiveVisitRequest Request { get; set; }

        public ArchiveVisitCommand(ArchiveVisitRequest request)
        {
            Request = request;
        }
    }

}
