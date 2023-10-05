using Clinic.Core.Appointments.Domain.Patients;
using Clinic.ViewModels.Appointments.Patients;
using FluentValidation;
using MediatR;

namespace Clinic.Core.Appointments.Application.Patients
{
    public class UpdatePatientCommandHandler
        : IRequestHandler<UpdatePatientCommand, PatientSummary>
    {
        private readonly IPatientRepository _patientRepository;

        public UpdatePatientCommandHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        }

        public async Task<PatientSummary> Handle(UpdatePatientCommand message, CancellationToken cancellationToken)
        {
            // TODO: Add Integration events to notify others
            var model = await _patientRepository.GetAsync(message.Request.Id) ?? 
                throw new ArgumentException($"No patient found {message.Request.Id}");
           
            model.SetCardNumber(message.Request.CardNumber);
            model.SetFullName(message.Request.FullName);
            model.SetGender(message.Request.Gender);
            model.SetPhoneNumber(message.Request.PhoneNumber);
            model.SetDateOfBirth(message.Request.DateOfBirth);
            model.SetEmail(message.Request.Email);
            _patientRepository.Update(model);

            var result = await _patientRepository.UnitOfWork.SaveEntitiesAsync();
            if (result == 0) return null;

            return message.ToResponse(model);
        }
    }

    /// <summary>
    /// Update Patient Command Model
    /// </summary>
    public class UpdatePatientCommand : IRequest<PatientSummary>
    {
        public UpdatePatient Request { get; set; }

        public UpdatePatientCommand(UpdatePatient request)
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

    public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
    {
        public UpdatePatientCommandValidator()
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
