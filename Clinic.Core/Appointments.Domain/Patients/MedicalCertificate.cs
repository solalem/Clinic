namespace Clinic.Core.Appointments.Domain.Patients
{
    public class MedicalCertificate
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public DateTimeOffset IssueDate { get; set; }
        public string Recipient { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
        public string CertificateText { get; set; } = string.Empty;
        public Patient? Patient { get; set; }
    }
}
