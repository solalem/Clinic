using FluentValidation;
using Clinic.ViewModels.Appointments.Visits;
using Clinic.Core.Appointments.Domain.Visits;
using MediatR;

namespace Clinic.Core.Appointments.Application.Commands.CreateVisitCommands
{
    public class CreateVisitCommandHandler
        : IRequestHandler<CreateVisitCommand, VisitSummary>
    {
        private readonly IVisitRepository _visitRepository;

        public CreateVisitCommandHandler(IMediator mediator,
            IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository ?? throw new ArgumentNullException(nameof(visitRepository));
        }

        public async Task<VisitSummary> Handle(CreateVisitCommand message, CancellationToken cancellationToken)
        {
            // TODO: Add Integration events to notify others
            var model = new Visit(message.Request.Date,
				message.Request.PatientId,
				message.Request.Physician,
				message.Request.Description);
            _visitRepository.Add(model);

            var result = await _visitRepository.UnitOfWork.SaveEntitiesAsync();
            if (result == 0) return null;

            return message.ToResponse(model);
        }
    }

    /// <summary>
    /// Visit Command Model
    /// </summary>
    public class CreateVisitCommand : IRequest<VisitSummary>
    {
        public CreateVisit Request { get; set; }

        public CreateVisitCommand(CreateVisit request)
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

    public class CreateVisitCommandValidator : AbstractValidator<CreateVisitCommand>
    {
        public CreateVisitCommandValidator()
        {
            // Insert all applicable rules
            // For example:
            // RuleFor(command => command.CardExpiration).NotEmpty().Must(BeValidExpirationDate).WithMessage("Please specify a valid card expiration date"); 
            RuleFor(command => command.Request.Date).NotEmpty();
            RuleFor(command => command.Request.PatientId).NotEmpty();
            RuleFor(command => command.Request.Physician).NotEmpty();
            RuleFor(command => command.Request.Description).NotEmpty();
        }
        // Add your rules here
        // For example
        //private bool BeValidExpirationDate(DateTime dateTime)
        //{
        //    return dateTime >= DateTime.UtcNow;
        //}
    }

}
