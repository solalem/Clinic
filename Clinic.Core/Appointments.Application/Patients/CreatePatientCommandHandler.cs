using MediatR;
using Clinic.Core.Appointments.Domain.Patients;
using Clinic.Shared;
using Clinic.ViewModels.Appointments.Patients;

namespace Clinic.Core.Appointments.Application.Patients
{
    public class CreatePatientCommandHandler
        : IRequestHandler<CreatePatientCommand, CreatePatientResponse>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IIdentityService _identityService;

        public CreatePatientCommandHandler(
            IPatientRepository patientRepository, 
            IIdentityService identityService)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<CreatePatientResponse> Handle(CreatePatientCommand command, CancellationToken cancellationToken)
        {
            var (success, error) = command.Validate();
            if (!success)
                return new CreatePatientResponse { Error = error };

            var model = new Patient(command.Request.CardNumber,
				command.Request.FullName,
				command.Request.Gender,
				command.Request.PhoneNumber,
				command.Request.DateOfBirth,
				command.Request.Email,
				command.Request.City,
				command.Request.MedicalHistory);
            _patientRepository.Add(model);

            try
            {
                await _patientRepository.UnitOfWork.SaveEntitiesAsync(_identityService.GetUserIdentity(), cancellationToken);
            }
            catch (Exception ex)
            {
                return new CreatePatientResponse { Error = ex.Message };
            }

            return new CreatePatientResponse 
            { 
                Succeed = true, 
                Data = PatientMapper.FromModel(model)
            };
        }
    }

    /// <summary>
    /// Patient Command Model
    /// </summary>
    public class CreatePatientCommand : IRequest<CreatePatientResponse>
    {
        public CreatePatientRequest Request { get; set; }

        public CreatePatientCommand(CreatePatientRequest request)
        {
            Request = request;
        }

        public (bool, string) Validate()
        {
            if (Request == null)
                return (false, "Request cannot be null");
            
            if(string.IsNullOrEmpty(Request.CardNumber)) return (false, "Card number should not be null"); ;
            if(string.IsNullOrEmpty(Request.FullName)) return (false, "Full Name should not be null"); ;
            if(string.IsNullOrEmpty(Request.Gender)) return (false, "Gender should not be null"); ;
            if(string.IsNullOrEmpty(Request.PhoneNumber)) return (false, "Phone number should not be null"); ;
            if(string.IsNullOrEmpty(Request.Email)) return (false, "Email should not be null"); ;

            return (true, null);
        }
    }
}
