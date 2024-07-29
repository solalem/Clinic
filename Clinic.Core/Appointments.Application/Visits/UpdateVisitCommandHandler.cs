using FluentValidation;
using MediatR;
using Clinic.ViewModels.Appointments.Visits;
using Clinic.Core.Appointments.Domain.Visits;

namespace Clinic.Core.Appointments.Application.Commands.UpdateVisitCommands
{
    public class UpdateVisitCommandHandler
        : IRequestHandler<UpdateVisitCommand, VisitSummary>
    {
        private readonly IVisitRepository _visitRepository;

        public UpdateVisitCommandHandler(IMediator mediator,
            IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository ?? throw new ArgumentNullException(nameof(visitRepository));
        }

        public async Task<VisitSummary> Handle(UpdateVisitCommand message, CancellationToken cancellationToken)
        {
            // TODO: Add Integration events to notify others
            var model = await _visitRepository.GetAsync(message.Request.Id) ??
                throw new ArgumentException($"No visit found {message.Request.Id}");

            //model.SetDate(message.Request.Date);
            //model.SetPatientId(message.Request.PatientId);
            model.SetPhysician(message.Request.Physician);
            model.SetDescription(message.Request.Description);
            foreach (var procedure in message.Request.Procedures)
            {
                model.AddProcedure(new Procedure
                {
                    Description = procedure.Description,
                    Name = procedure.Name,
                });
            }
            _visitRepository.Update(model);

            var result = await _visitRepository.UnitOfWork.SaveEntitiesAsync();
            if (result == 0) return null;

            return message.ToResponse(model);
        }
    }

    /// <summary>
    /// Visit Command Model
    /// </summary>
    public class UpdateVisitCommand : IRequest<VisitSummary>
    {
        public UpdateVisit Request { get; set; }

        public UpdateVisitCommand(UpdateVisit request)
        {
            Request = request;
        }

        public VisitSummary ToResponse(Visit model)
        {
            return new VisitSummary
            {
                Id = model.Id,
                Date = model.Date,
				PatientId = model.PatientId,
				Physician = model.Physician,
				Description = model.Description
            };
        }

    }

    public class UpdateVisitCommandValidator : AbstractValidator<UpdateVisitCommand>
    {
        public UpdateVisitCommandValidator()
        {
            // Insert all applicable rules
            // For example:
            // RuleFor(command => command.CardExpiration).NotEmpty().Must(BeValidExpirationDate).WithMessage("Please specify a valid card expiration date"); 
            RuleFor(command => command.Request.PatientId).NotEmpty();
            RuleFor(command => command.Request.Physician).NotEmpty();
            RuleFor(command => command.Request.Description).NotEmpty();
            RuleFor(command => command.Request.Procedures).NotEmpty();
        }
        // Add your rules here
        // For example
        //private bool BeValidExpirationDate(DateTime dateTime)
        //{
        //    return dateTime >= DateTime.UtcNow;
        //}
    }

}
