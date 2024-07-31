using Clinic.Core.Appointments.Domain.Patients;
using Clinic.Core.Appointments.Persistence.Patients;
using Clinic.ViewModels.Appointments.Patients;
using FluentValidation;
using MediatR;

namespace Clinic.Core.Appointments.Application.Patients
{
    public class GetPatientCommandHandler
        : IRequestHandler<GetPatientCommand, PatientDetail>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientCommandHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        }

        public async Task<PatientDetail> Handle(GetPatientCommand message, CancellationToken cancellationToken)
        {
            // TODO: Add Integration events to notify others
            var model = await _patientRepository.GetAsync(message.Request.Id) ?? 
                throw new ArgumentException($"No patient found {message.Request.Id}");

            return GetPatientCommand.ToResponse(model);
        }
    }

    /// <summary>
    /// Get Patient Command Model
    /// </summary>
    public class GetPatientCommand : IRequest<PatientDetail>
    {
        public GetPatient Request { get; set; }

        public GetPatientCommand(GetPatient request)
        {
            Request = request;
        }

        public static PatientDetail ToResponse(Patient model)
        {
            return new PatientDetail
            {
                Id = model.Id,
                CardNumber = model.CardNumber,
                FullName = model.FullName,
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                City = model.City,
                RegisterationDate = model.RegisterationDate,
                MedicalHistory = model.MedicalHistory
            };
        }

    }

}
