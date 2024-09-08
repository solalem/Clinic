using Clinic.Core.Appointments.Domain.Patients;
using Clinic.ViewModels.Appointments.Patients;

namespace Clinic.Core.Appointments.Application.Patients
{
    public static class PatientMapper
    {
        public static IEnumerable<PatientSummary> FromModel(IEnumerable<Patient> models)
        {
            return models.Select(x => FromModel(x)).ToList();
        }

        public static PatientDetail FromModel(Patient model)
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
                MedicalHistory = model.MedicalHistory,
                RegisterationDate = model.RegisterationDate,
            };
        }

    }
}
