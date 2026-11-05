using Clinic.Core.Appointments.Domain.Visits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Core.Appointments.Persistence.Visits
{
    public class ProcedureCatalogEntityTypeConfiguration : IEntityTypeConfiguration<ProcedureCatalogItem>
    {
        public void Configure(EntityTypeBuilder<ProcedureCatalogItem> config)
        {
            config.ToTable("procedure_catalog", AppointmentsDbContext.DEFAULT_SCHEMA);
            config.HasKey(item => item.Id);
            config.Property(item => item.Name).IsRequired();
            config.Property(item => item.Code).IsRequired();
            config.Property(item => item.Price).HasColumnType("TEXT").IsRequired();
            config.HasData(
                new ProcedureCatalogItem { Id = 1, Name = "Card", Code = "CD 1", Price = 400m },
                new ProcedureCatalogItem { Id = 2, Name = "Scaling and polishing", Code = "SP1", Price = 2500m },
                new ProcedureCatalogItem { Id = 3, Name = "Hard scaling and polishing", Code = "SP2", Price = 3000m },
                new ProcedureCatalogItem { Id = 4, Name = "Bleaching with polishing", Code = "BWP1", Price = 10000m },
                new ProcedureCatalogItem { Id = 5, Name = "Milk tooth extraction", Code = "MTE 1", Price = 700m },
                new ProcedureCatalogItem { Id = 6, Name = "Simple tooth extraction", Code = "STE1", Price = 800m },
                new ProcedureCatalogItem { Id = 7, Name = "Composite filling", Code = "CF1", Price = 4000m },
                new ProcedureCatalogItem { Id = 8, Name = "Apicectomy", Code = "APC1", Price = 8000m },
                new ProcedureCatalogItem { Id = 9, Name = "Root canal treatment", Code = "RCT 1", Price = 7000m },
                new ProcedureCatalogItem { Id = 10, Name = "Digital X ray", Code = "DXR1", Price = 500m },
                new ProcedureCatalogItem { Id = 11, Name = "Hard tooth extraction", Code = "HTE1", Price = 1800m },
                new ProcedureCatalogItem { Id = 12, Name = "Fixed retainer per arch", Code = "FR1", Price = 3000m },
                new ProcedureCatalogItem { Id = 13, Name = "Orthodontic treatment simple", Code = "OTS1", Price = 50000m },
                new ProcedureCatalogItem { Id = 14, Name = "Orthodontic treatment complex", Code = "OTC 1", Price = 70000m },
                new ProcedureCatalogItem { Id = 15, Name = "Orthodontic treatment moderate", Code = "OTM1", Price = 60000m },
                new ProcedureCatalogItem { Id = 16, Name = "Orthodontic treatment simple half", Code = "OTSH1", Price = 25000m },
                new ProcedureCatalogItem { Id = 17, Name = "Orthodontic treatment complex half", Code = "OTCH1", Price = 35000m },
                new ProcedureCatalogItem { Id = 18, Name = "Orthodontic treatment moderate half", Code = "OTMH", Price = 30000m },
                new ProcedureCatalogItem { Id = 19, Name = "Porcelain veneer", Code = "PV1", Price = 30000m },
                new ProcedureCatalogItem { Id = 20, Name = "Ceramic crown", Code = "CC1", Price = 11000m },
                new ProcedureCatalogItem { Id = 21, Name = "Zirconium crown", Code = "ZC1", Price = 22000m },
                new ProcedureCatalogItem { Id = 22, Name = "E max crown", Code = "EMC1", Price = 24000m },
                new ProcedureCatalogItem { Id = 23, Name = "Zirconium veneer", Code = "ZV1", Price = 28000m },
                new ProcedureCatalogItem { Id = 24, Name = "Implant per tooth", Code = "IM1", Price = 100000m },
                new ProcedureCatalogItem { Id = 25, Name = "Retainer transparent", Code = "RT1", Price = 3000m },
                new ProcedureCatalogItem { Id = 26, Name = "Removable denture (acrylic per tooth)", Code = "RDA1", Price = 2500m },
                new ProcedureCatalogItem { Id = 27, Name = "Removable denture (flexible per tooth)", Code = "RDF1", Price = 7500m },
                new ProcedureCatalogItem { Id = 28, Name = "Splinting per tooth with composite", Code = "SWC1", Price = 1500m },
                new ProcedureCatalogItem { Id = 29, Name = "Arch bar splinting", Code = "ABS 1", Price = 4000m },
                new ProcedureCatalogItem { Id = 30, Name = "Intermaxillary fixation", Code = "IMF1", Price = 15000m },
                new ProcedureCatalogItem { Id = 31, Name = "Temporary filling", Code = "TF1", Price = 500m },
                new ProcedureCatalogItem { Id = 32, Name = "GI filling for pedi", Code = "GI1", Price = 1000m },
                new ProcedureCatalogItem { Id = 33, Name = "Pulpectomy", Code = "PPC1", Price = 2000m },
                new ProcedureCatalogItem { Id = 34, Name = "Composite veneer per tooth", Code = "CV1", Price = 3000m },
                new ProcedureCatalogItem { Id = 35, Name = "Posterior tooth extraction", Code = "PTE1", Price = 2700m },
                new ProcedureCatalogItem { Id = 36, Name = "Flap raising and closure", Code = "FRC1", Price = 7000m },
                new ProcedureCatalogItem { Id = 37, Name = "Root canal treatment posterior", Code = "RCTP1", Price = 9000m },
                new ProcedureCatalogItem { Id = 38, Name = "Suturing", Code = "S1", Price = 1200m },
                new ProcedureCatalogItem { Id = 39, Name = "Biopsy", Code = "BI1", Price = 6000m },
                new ProcedureCatalogItem { Id = 40, Name = "Gingivectomy", Code = "GNG1", Price = 2000m });
        }
    }
}
