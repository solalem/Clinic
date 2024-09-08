using Clinic.Core.Appointments.Domain.Patients;
using Clinic.Shared;
using Clinic.ViewModels.Appointments.Patients;
using MediatR;

namespace Clinic.Core.Appointments.Application.Patients
{
    public class ArchivePatientCommandHandler
        : IRequestHandler<ArchivePatientCommand, ArchivePatientResponse>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IIdentityService _identityService;

        public ArchivePatientCommandHandler(
            IPatientRepository patientRepository, 
            IIdentityService identityService)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<ArchivePatientResponse> Handle(ArchivePatientCommand message, CancellationToken cancellationToken)
        {
            try
            {
                _patientRepository.Delete(message.Request.Id);
                await _patientRepository.UnitOfWork.SaveEntitiesAsync(_identityService.GetUserIdentity(), cancellationToken);
          
                return new ArchivePatientResponse
                {
                    Succeed = true
                };
            }
            catch (Exception ex)
            {
                // TODO: log
                return new ArchivePatientResponse { Error = ex.Message };
            }
        }
    }

    /// <summary>
    /// Patient Command Model
    /// </summary>
    public class ArchivePatientCommand : IRequest<ArchivePatientResponse>
    {
        public ArchivePatientRequest Request { get; set; }

        public ArchivePatientCommand(ArchivePatientRequest request)
        {
            Request = request;
        }
    }

}
