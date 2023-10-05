using Clinic.Core.Appointments.Domain.Patients;
using Clinic.ViewModels.Appointments.Patients;
using MediatR;

namespace Clinic.Core.Appointments.Application.Patients
{
    public class GetPatientsCommandHandler
        : IRequestHandler<GetPatientsCommand, PatientList>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientsCommandHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        }

        public async Task<PatientList> Handle(GetPatientsCommand message, CancellationToken cancellationToken)
        {
            // TODO: Add Integration events to notify others
            var models = await _patientRepository.GetManyAsync(message.Request.PaginationInfo.Index, message.Request.PaginationInfo.PageSize, message.Request.PaginationInfo.SearchString);

            return GetPatientsCommand.ToResponse(message.Request, models);
        }
    }

    /// <summary>
    /// Get Patient Command Model
    /// </summary>
    public class GetPatientsCommand : IRequest<PatientList>
    {
        public GetPatients Request { get; set; }

        public GetPatientsCommand(GetPatients request)
        {
            Request = request;
        }

        public static PatientList ToResponse(GetPatients request, IEnumerable<Patient> models)
        {
            var list = new PatientList();
            list.Items = models.Select(model =>
               new PatientSummary
               {
                   Id = model.Id,
                   CardNumber = model.CardNumber,
                   FullName = model.FullName,
                   Gender = model.Gender,
                   PhoneNumber = model.PhoneNumber,
                   DateOfBirth = model.DateOfBirth,
                   Email = model.Email
               }).ToList();

            list.PaginationInfo = request.PaginationInfo;
            return list;
        }

    }

}
