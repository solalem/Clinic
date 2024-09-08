using MediatR;
using Clinic.Core.Appointments.Domain.Visits;
using Clinic.Shared;
using Clinic.ViewModels.Appointments.Visits;

namespace Clinic.Core.Appointments.Application.Visits
{
    public class UpdateVisitCommandHandler
        : IRequestHandler<UpdateVisitCommand, UpdateVisitResponse>
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IIdentityService _identityService;

        public UpdateVisitCommandHandler(
            IVisitRepository visitRepository, 
            IIdentityService identityService)
        {
            _visitRepository = visitRepository ?? throw new ArgumentNullException(nameof(visitRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<UpdateVisitResponse> Handle(UpdateVisitCommand command, CancellationToken cancellationToken)
        {
            var (success, error) = command.Validate();
            if (!success)
                return new UpdateVisitResponse { Error = error };
        
            var model = await _visitRepository.GetAsync(command.Request.Id);
			model.SetPhysician(command.Request.Physician);
			model.SetPresentIllness(command.Request.PresentIllness);
            foreach (var procedure in command.Request.Procedures)
            {
                model.AddProcedure(new Procedure
                {
                    Description = procedure.Description,
                    Name = procedure.Name,
                });
            }
            _visitRepository.Update(model);

            try
            {
                await _visitRepository.UnitOfWork.SaveEntitiesAsync(_identityService.GetUserIdentity(), cancellationToken);
            }
            catch (Exception ex)
            {
                return new UpdateVisitResponse { Error = ex.Message };
            }

            return new UpdateVisitResponse
            {
                Succeed = true,
                Data = VisitMapper.FromModel(model)
            };
        }
    }

    /// <summary>
    /// Update Visit Command Model
    /// </summary>
    public class UpdateVisitCommand : IRequest<UpdateVisitResponse>
    {
        public UpdateVisitRequest Request { get; set; }

        public UpdateVisitCommand(UpdateVisitRequest request)
        {
            Request = request;
        }

        public (bool, string) Validate()
        {
            if (Request == null)
                return (false, "Request cannot be null");

            if (string.IsNullOrEmpty(Request.PresentIllness)) return (false, "Present Illness cannot be null");
            if (string.IsNullOrEmpty(Request.Physician)) return (false, "Physician cannot be null");

            return (true, null);
        }
    }

}
