namespace Clinic.ViewModels.Appointments.Patients
{
    public class CreatePatient
    {
        public String CardNumber { get; set; }

        public String FullName { get; set; }

        public String Gender { get; set; }

        public String PhoneNumber { get; set; }

        public DateTimeOffset? DateOfBirth { get; set; }

        public String Email { get; set; }
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
    }

}