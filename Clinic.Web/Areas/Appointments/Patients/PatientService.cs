using Blazorise;
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

        public async Task<CreatePatientResponse> CreateAsync(CreatePatientRequest request)
        {
            return await _mediator.Send(new CreatePatientCommand(request));
        }

        public async Task<UpdatePatientResponse> UpdateAsync(UpdatePatientRequest request)
        {
            return await _mediator.Send(new UpdatePatientCommand(request));
        }

        public async Task<ArchivePatientResponse> DeleteAsync(Guid id)
        {
            return await _mediator.Send(new ArchivePatientCommand(new(id)));
        }

        public async Task<GetPatientResponse> GetAsync(GetPatientRequest request)
        {
            return await _mediator.Send(new GetPatientCommand(request));
        }

        public async Task<GetPatientsResponse> ListAsync(GetPatientsRequest request)
        {
            return await _mediator.Send(new GetPatientsCommand(request));
        }

    }
}
