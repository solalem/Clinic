using Clinic.Core.Appointments.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Core.Appointments.Persistence.Patients
{
    public class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> config)
        {
            config.ToTable("patients", AppointmentsDbContext.DEFAULT_SCHEMA);
            config.HasKey(o => o.Id);

            config.Property(o => o.Id)
                .HasColumnName("Id")
                .ValueGeneratedNever()
           ; 
            config.Property(o => o.CardNumber)
                .HasColumnName("CardNumber")
                .IsRequired();

            config.Property(o => o.FullName)
                .HasColumnName("FullName")
                .IsRequired();

            config.Property(o => o.Gender)
                .HasColumnName("Gender")
                .IsRequired();

            config.Property(o => o.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .IsRequired();

            config.Property(o => o.DateOfBirth)
                .HasColumnName("DateOfBirth")
                .IsRequired(false);

            config.Property(o => o.Email)
                .HasColumnName("Email")
                .IsRequired();
            
            config.Property(o => o.City)
                .HasColumnName("City")
                .IsRequired(false);
            
            config.Property(o => o.MedicalHistory)
                .HasColumnName("MedicalHistory")
                .IsRequired(false);
            
            config.Property(o => o.RegisterationDate)
                .HasColumnName("RegisterationDate")
                .IsRequired(false);

        }
    }
}
