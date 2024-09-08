using Clinic.Core.Appointments.Domain.Visits;
using Clinic.Shared;
using Clinic.ViewModels.Appointments.Visits;
using MediatR;

namespace Clinic.Core.Appointments.Application.Visits
{
    public class CreateVisitCommandHandler
        : IRequestHandler<CreateVisitCommand, CreateVisitResponse>
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IIdentityService _identityService;

        public CreateVisitCommandHandler(
            IVisitRepository visitRepository, 
            IIdentityService identityService)
        {
            _visitRepository = visitRepository ?? throw new ArgumentNullException(nameof(visitRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<CreateVisitResponse> Handle(CreateVisitCommand command, CancellationToken cancellationToken)
        {
            var (success, error) = command.Validate();
            if (!success)
                return new CreateVisitResponse { Error = error };

            var model = new Visit(
                command.Request.PatientId,
				command.Request.Physician,
				command.Request.PresentIllness);
            _visitRepository.Add(model);

            try
            {
                await _visitRepository.UnitOfWork.SaveEntitiesAsync(_identityService.GetUserIdentity(), cancellationToken);
            }
            catch (Exception ex)
            {
                return new CreateVisitResponse { Error = ex.Message };
            }

            return new CreateVisitResponse 
            { 
                Succeed = true, 
                Data = VisitMapper.FromModel(model)
            };
        }
    }

    /// <summary>
    /// Visit Command Model
    /// </summary>
    public class CreateVisitCommand : IRequest<CreateVisitResponse>
    {
        public CreateVisitRequest Request { get; set; }

        public CreateVisitCommand(CreateVisitRequest request)
        {
            Request = request;
        }

        public (bool, string) Validate()
        {
            if (Request == null)
                return (false, "Request cannot be null");
            //if (string.IsNullOrEmpty(Request.Name)) return (false, "Name cannot be null");

            return (true, null);
        }
    }
}
