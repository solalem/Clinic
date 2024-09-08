using Clinic.Core.Appointments.Domain.Patients;
using Clinic.Shared;
using Clinic.ViewModels.Appointments.Patients;
using MediatR;

namespace Clinic.Core.Appointments.Application.Patients
{
    public class GetPatientCommandHandler
        : IRequestHandler<GetPatientCommand, GetPatientResponse>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IIdentityService _identityService;

        public GetPatientCommandHandler(
            IPatientRepository patientRepository, 
            IIdentityService identityService)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<GetPatientResponse> Handle(GetPatientCommand message, CancellationToken cancellationToken)
        {
            var model = await _patientRepository.GetAsync(message.Request.Id);

            return new GetPatientResponse
            {
                Succeed = true,
                Data = PatientMapper.FromModel(model)
            };
        }
    }

    /// <summary>
    /// Patient Command Model
    /// </summary>
    public class GetPatientCommand : IRequest<GetPatientResponse>
    {
        public GetPatientRequest Request { get; set; }

        public GetPatientCommand(GetPatientRequest request)
        {
            Request = request;
        }
    }
}
