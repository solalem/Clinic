using Clinic.Core.Appointments.Persistence.Visits;
using MediatR;

namespace Clinic.Core.Appointments.Application.Commands.ArchiveVisitCommands
{
    public class ArchiveVisitCommandHandler
        : IRequestHandler<ArchiveVisitCommand, int>
    {
        private readonly IVisitRepository _visitRepository;

        public ArchiveVisitCommandHandler(IMediator mediator,
            IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository ?? throw new ArgumentNullException(nameof(visitRepository));
        }

        public async Task<int> Handle(ArchiveVisitCommand message, CancellationToken cancellationToken)
        {
            // TODO: Add Integration event to notify others
            _visitRepository.Delete(message.Id);

            return await _visitRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }

    /// <summary>
    /// Visit Command Model
    /// </summary>
    public class ArchiveVisitCommand : IRequest<int>
    {
        public Guid Id { get; set; }

    }

}
