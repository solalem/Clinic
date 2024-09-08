using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Clinic.Core.Appointments.Domain.Visits;
using System.Text.Json;

namespace Clinic.Core.Appointments.Persistence.Visits
{
    public class VisitEntityTypeConfiguration : IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> config)
        {
            config.ToTable("visits", AppointmentsDbContext.DEFAULT_SCHEMA);
            config.HasKey(o => o.Id);
            config.Ignore(b => b.DomainEvents);

            config.Property(o => o.Id)
                .HasColumnName("Id")
                .ValueGeneratedNever();

            config.Property(o => o.Date)
                .HasColumnName("Date")
                .IsRequired();

            config.Property(o => o.PatientId)
                .HasColumnName("PatientId")
                .IsRequired();

            config.Property(o => o.Physician)
                .HasColumnName("Physician")
                .IsRequired();

            config.Property(o => o.PresentIllness)
                .HasColumnName("PresentIllness")
                .IsRequired();
            config.Property(o => o.Procedures)
                .HasColumnName("Procedures")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<IReadOnlyCollection<Procedure>>(v, new JsonSerializerOptions()))
                .IsRequired();
        }
    }
}
