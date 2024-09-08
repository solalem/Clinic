namespace Clinic.ViewModels.Appointments.Patients
{
    public class CreatePatientRequest
    {
        public Guid Id { get; set; }
        public String CardNumber { get; set; }
        public String FullName { get; set; }
        public String Gender { get; set; }
        public String PhoneNumber { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; } = DateTimeOffset.Now.AddYears(-25);
        public Decimal Age { get; set; }
        public String Email { get; set; }
        public String City { get; set; }
        public String MedicalHistory { get; set; }

    }

    public class CreatePatientResponse : ApiResponse<PatientDetail>
    {
    }
}
