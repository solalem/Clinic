using MediatR;
using Clinic.Core.Appointments.Persistence.Patients;

namespace Clinic.Core.Appointments.Application.Patients
{
    public class ArchivePatientCommandHandler
        : IRequestHandler<ArchivePatientCommand, int>
    {
        private readonly IPatientRepository _patientRepository;

        public ArchivePatientCommandHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        }

        public async Task<int> Handle(ArchivePatientCommand message, CancellationToken cancellationToken)
        {
            // TODO: Add Integration event to notify others
            _patientRepository.Delete(message.Id);

            return await _patientRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }

    /// <summary>
    /// Archive Patient Command Model
    /// </summary>
    public class ArchivePatientCommand : IRequest<int>
    {
        public Guid Id { get; set; }

    }

}
