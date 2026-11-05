using Clinic.Core.Appointments.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Core.Appointments.Persistence.Patients
{
    public class MedicalCertificateEntityTypeConfiguration : IEntityTypeConfiguration<MedicalCertificate>
    {
        public void Configure(EntityTypeBuilder<MedicalCertificate> config)
        {
            config.ToTable("medical_certificates", AppointmentsDbContext.DEFAULT_SCHEMA);
            config.HasKey(certificate => certificate.Id);
            config.Property(certificate => certificate.Recipient).IsRequired();
            config.Property(certificate => certificate.Purpose).IsRequired();
            config.Property(certificate => certificate.CertificateText).IsRequired();
            config.Property(certificate => certificate.IssueDate)
                .HasConversion(
                    issueDate => issueDate.UtcTicks,
                    utcTicks => new DateTimeOffset(utcTicks, TimeSpan.Zero))
                .HasColumnType("INTEGER")
                .IsRequired();
            config.HasOne(certificate => certificate.Patient)
                .WithMany()
                .HasForeignKey(certificate => certificate.PatientId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
