using Clinic.Core.Appointments.Domain.Patients;
using Clinic.Core.Appointments.Persistence.Patients;
using Clinic.ViewModels.Appointments.Patients;
using FluentValidation;
using MediatR;

namespace Clinic.Core.Appointments.Application.Patients
{
    public class CreatePatientCommandHandler
        : IRequestHandler<CreatePatientCommand, PatientSummary>
    {
        private readonly IPatientRepository _patientRepository;

        public CreatePatientCommandHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        }

        public async Task<PatientSummary> Handle(CreatePatientCommand message, CancellationToken cancellationToken)
        {
            // TODO: Add Integration events to notify others
            var model = new Patient(message.Request.CardNumber,
				message.Request.FullName,
				message.Request.Gender,
				message.Request.PhoneNumber,
				message.Request.DateOfBirth,
				message.Request.Email);
            _patientRepository.Add(model);

            var result = await _patientRepository.UnitOfWork.SaveEntitiesAsync();
            if (result == 0) return null;

            return message.ToResponse(model);
        }
    }

    /// <summary>
    /// Create Patient Command Model
    /// </summary>
    public class CreatePatientCommand : IRequest<PatientSummary>
    {
        public CreatePatient Request { get; set; }

        public CreatePatientCommand(CreatePatient request)
        {
            Request = request;
        }

        public PatientSummary ToResponse(Patient model)
        {
            return new PatientSummary
            {
                Id = model.Id,
                CardNumber = model.CardNumber,
				FullName = model.FullName,
				Gender = model.Gender,
				PhoneNumber = model.PhoneNumber,
				DateOfBirth = model.DateOfBirth,
				Email = model.Email
            };
        }

    }

    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator()
        {
            // Insert all applicable rules
            // For example:
            // RuleFor(command => command.CardExpiration).NotEmpty().Must(BeValidExpirationDate).WithMessage("Please specify a valid card expiration date"); 
            RuleFor(command => command.Request.CardNumber).NotEmpty();
            RuleFor(command => command.Request.FullName).NotEmpty();
            RuleFor(command => command.Request.Gender).NotEmpty();
            RuleFor(command => command.Request.PhoneNumber).NotEmpty();
            RuleFor(command => command.Request.Email).NotEmpty();
        }
        // Add your rules here
        // For example
        //private bool BeValidExpirationDate(DateTime dateTime)
        //{
        //    return dateTime >= DateTime.UtcNow;
        //}
    }

}
