using Clinic.Core.Appointments.Domain.Visits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Core.Appointments.Persistence.Visits
{
    public class InvestigationCatalogEntityTypeConfiguration : IEntityTypeConfiguration<InvestigationCatalogItem>
    {
        public void Configure(EntityTypeBuilder<InvestigationCatalogItem> config)
        {
            config.ToTable("investigation_catalog", AppointmentsDbContext.DEFAULT_SCHEMA);
            config.HasKey(item => item.Id);
            config.Property(item => item.Name).IsRequired();
            config.HasData(
                new InvestigationCatalogItem { Id = 1, Name = "IOPA" },
                new InvestigationCatalogItem { Id = 2, Name = "OPG" },
                new InvestigationCatalogItem { Id = 3, Name = "CBCT" },
                new InvestigationCatalogItem { Id = 4, Name = "CT scan" },
                new InvestigationCatalogItem { Id = 5, Name = "MRI" },
                new InvestigationCatalogItem { Id = 6, Name = "CBC" },
                new InvestigationCatalogItem { Id = 7, Name = "OFT" },
                new InvestigationCatalogItem { Id = 8, Name = "FBS" },
                new InvestigationCatalogItem { Id = 9, Name = "Hga1c" },
                new InvestigationCatalogItem { Id = 10, Name = "INR" });
        }
    }
}
