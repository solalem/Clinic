using MediatR;
using Clinic.Core.Appointments.Domain.Patients;
using Clinic.Shared;
using Clinic.ViewModels.Appointments.Patients;

namespace Clinic.Core.Appointments.Application.Patients
{
    public class UpdatePatientCommandHandler
        : IRequestHandler<UpdatePatientCommand, UpdatePatientResponse>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IIdentityService _identityService;

        public UpdatePatientCommandHandler(
            IPatientRepository patientRepository, 
            IIdentityService identityService)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<UpdatePatientResponse> Handle(UpdatePatientCommand command, CancellationToken cancellationToken)
        {
            var (success, error) = command.Validate();
            if (!success)
                return new UpdatePatientResponse { Error = error };
        
            var model = await _patientRepository.GetAsync(command.Request.Id);
            model.SetCardNumber(command.Request.CardNumber);
			model.SetFullName(command.Request.FullName);
			model.SetGender(command.Request.Gender);
			model.SetPhoneNumber(command.Request.PhoneNumber);
			model.SetDateOfBirth(command.Request.DateOfBirth);
			model.SetEmail(command.Request.Email);
			model.SetCity(command.Request.City);
			model.SetMedicalHistory(command.Request.MedicalHistory);
            _patientRepository.Update(model);

            try
            {
                await _patientRepository.UnitOfWork.SaveEntitiesAsync(_identityService.GetUserIdentity(), cancellationToken);
            }
            catch (Exception ex)
            {
                return new UpdatePatientResponse { Error = ex.Message };
            }

            return new UpdatePatientResponse
            {
                Succeed = true,
                Data = PatientMapper.FromModel(model)
            };
        }
    }

    /// <summary>
    /// Update Patient Command Model
    /// </summary>
    public class UpdatePatientCommand : IRequest<UpdatePatientResponse>
    {
        public UpdatePatientRequest Request { get; set; }

        public UpdatePatientCommand(UpdatePatientRequest request)
        {
            Request = request;
        }

        public (bool, string) Validate()
        {
            if (Request == null)
                return (false, "Request cannot be null");

            if (string.IsNullOrEmpty(Request.CardNumber)) return (false, "Card number should not be null"); ;
            if (string.IsNullOrEmpty(Request.FullName)) return (false, "Full Name should not be null"); ;
            if (string.IsNullOrEmpty(Request.Gender)) return (false, "Gender should not be null"); ;
            if (string.IsNullOrEmpty(Request.PhoneNumber)) return (false, "Phone number should not be null"); ;
            if (string.IsNullOrEmpty(Request.Email)) return (false, "Email should not be null"); ;

            return (true, null);
        }
    }

}
