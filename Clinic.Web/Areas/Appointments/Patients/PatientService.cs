using Clinic.Core.Appointments.Application.Patients;
using Clinic.ViewModels.Appointments.Patients;
using MediatR;

namespace Clinic.Web.Areas.Appointments.Patients
{
    public class PatientService: IPatientService
    {
        private readonly IMediator _mediator;

        public PatientService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<PatientSummary> CreateAsync(CreatePatient request)
        {
            return await _mediator.Send(new CreatePatientCommand(request));
        }

        public async Task<PatientSummary> UpdateAsync(UpdatePatient request)
        {
            return await _mediator.Send(new UpdatePatientCommand(request));
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            return await _mediator.Send(new ArchivePatientCommand { Id = id });
        }

        public async Task<PatientDetail> GetAsync(GetPatient request)
        {
            return await _mediator.Send(new GetPatientCommand(request));
        }

        public async Task<PatientList> ListAsync(GetPatients request)
        {
            return await _mediator.Send(new GetPatientsCommand(request));
        }

    }
}
