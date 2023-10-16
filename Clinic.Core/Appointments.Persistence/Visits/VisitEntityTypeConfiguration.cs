using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Clinic.Core.Appointments.Domain.Visits;
using Clinic.Core.Appointments.Domain.Patients;

namespace Clinic.Core.Appointments.Persistence
{
    public class VisitEntityTypeConfiguration : IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> config)
        {
            config.ToTable("visits", AppointmentsDbContext.DEFAULT_SCHEMA);
            config.HasKey(o => o.Id);
            //config.Ignore(b => b.DomainEvents);

            config.Property(o => o.Id)
                .HasColumnName("Id")
                .ValueGeneratedNever()
           ; 
            config.Property(o => o.Date)
                .HasColumnName("Date")
                .IsRequired();

            config.Property(o => o.PatientId)
                .HasColumnName("PatientId")
                .IsRequired();

            config.HasOne<Patient>()
               .WithMany()
               .HasForeignKey(y => y.PatientId) 
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

            config.Property(o => o.Physician)
                .HasColumnName("Physician")
                .IsRequired();

            config.Property(o => o.Description)
                .HasColumnName("Description")
                .IsRequired();
            config.Property(o => o.Procedures)
                .HasColumnName("Procedures")
                .HasColumnType("jsonb").IsRequired();

        }
    }
}
