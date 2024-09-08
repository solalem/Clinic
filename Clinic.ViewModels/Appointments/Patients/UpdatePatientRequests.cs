namespace Clinic.ViewModels.Appointments.Patients
{
    public class UpdatePatientRequest
    {
        public Guid Id { get; set; }

        public String CardNumber { get; set; }

        public String FullName { get; set; }

        public String Gender { get; set; }

        public String PhoneNumber { get; set; }

        public DateTimeOffset? DateOfBirth { get; set; }

        public String Email { get; set; }

        public String City { get; set; }
        public string MedicalHistory { get; set; }
    }

    public class UpdatePatientResponse : ApiResponse<PatientDetail>
    {
    }

}
