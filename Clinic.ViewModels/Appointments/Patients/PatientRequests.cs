namespace Clinic.ViewModels.Appointments.Patients
{
    public class CreatePatient
    {
        public String CardNumber { get; set; }

        public String FullName { get; set; }

        public String Gender { get; set; } = "Male";

        public String PhoneNumber { get; set; }

        public DateTimeOffset? DateOfBirth { get; set; } = DateTimeOffset.Now.AddYears(-10);

        public String Email { get; set; }

        public String City { get; set; }
        public string MedicalHistory { get; set; }
    }

    public class UpdatePatient
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


    public class GetPatient
    {
        public Guid Id { get; set; }
    }

    public class GetPatients
    {
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    }
}