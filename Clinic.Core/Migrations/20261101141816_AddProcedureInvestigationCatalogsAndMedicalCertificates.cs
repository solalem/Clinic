using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clinic.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddProcedureInvestigationCatalogsAndMedicalCertificates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Investigations",
                schema: "Appointments",
                table: "visits",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.CreateTable(
                name: "investigation_catalog",
                schema: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_investigation_catalog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "medical_certificates",
                schema: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PatientId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IssueDate = table.Column<long>(type: "INTEGER", nullable: false),
                    Recipient = table.Column<string>(type: "TEXT", nullable: false),
                    Purpose = table.Column<string>(type: "TEXT", nullable: false),
                    CertificateText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical_certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medical_certificates_patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Appointments",
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "procedure_catalog",
                schema: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_procedure_catalog", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "Appointments",
                table: "investigation_catalog",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "IOPA" },
                    { 2, "OPG" },
                    { 3, "CBCT" },
                    { 4, "CT scan" },
                    { 5, "MRI" },
                    { 6, "CBC" },
                    { 7, "OFT" },
                    { 8, "FBS" },
                    { 9, "Hga1c" },
                    { 10, "INR" }
                });

            migrationBuilder.InsertData(
                schema: "Appointments",
                table: "procedure_catalog",
                columns: new[] { "Id", "Code", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "CD 1", "Card", 400m },
                    { 2, "SP1", "Scaling and polishing", 2500m },
                    { 3, "SP2", "Hard scaling and polishing", 3000m },
                    { 4, "BWP1", "Bleaching with polishing", 10000m },
                    { 5, "MTE 1", "Milk tooth extraction", 700m },
                    { 6, "STE1", "Simple tooth extraction", 800m },
                    { 7, "CF1", "Composite filling", 4000m },
                    { 8, "APC1", "Apicectomy", 8000m },
                    { 9, "RCT 1", "Root canal treatment", 7000m },
                    { 10, "DXR1", "Digital X ray", 500m },
                    { 11, "HTE1", "Hard tooth extraction", 1800m },
                    { 12, "FR1", "Fixed retainer per arch", 3000m },
                    { 13, "OTS1", "Orthodontic treatment simple", 50000m },
                    { 14, "OTC 1", "Orthodontic treatment complex", 70000m },
                    { 15, "OTM1", "Orthodontic treatment moderate", 60000m },
                    { 16, "OTSH1", "Orthodontic treatment simple half", 25000m },
                    { 17, "OTCH1", "Orthodontic treatment complex half", 35000m },
                    { 18, "OTMH", "Orthodontic treatment moderate half", 30000m },
                    { 19, "PV1", "Porcelain veneer", 30000m },
                    { 20, "CC1", "Ceramic crown", 11000m },
                    { 21, "ZC1", "Zirconium crown", 22000m },
                    { 22, "EMC1", "E max crown", 24000m },
                    { 23, "ZV1", "Zirconium veneer", 28000m },
                    { 24, "IM1", "Implant per tooth", 100000m },
                    { 25, "RT1", "Retainer transparent", 3000m },
                    { 26, "RDA1", "Removable denture (acrylic per tooth)", 2500m },
                    { 27, "RDF1", "Removable denture (flexible per tooth)", 7500m },
                    { 28, "SWC1", "Splinting per tooth with composite", 1500m },
                    { 29, "ABS 1", "Arch bar splinting", 4000m },
                    { 30, "IMF1", "Intermaxillary fixation", 15000m },
                    { 31, "TF1", "Temporary filling", 500m },
                    { 32, "GI1", "GI filling for pedi", 1000m },
                    { 33, "PPC1", "Pulpectomy", 2000m },
                    { 34, "CV1", "Composite veneer per tooth", 3000m },
                    { 35, "PTE1", "Posterior tooth extraction", 2700m },
                    { 36, "FRC1", "Flap raising and closure", 7000m },
                    { 37, "RCTP1", "Root canal treatment posterior", 9000m },
                    { 38, "S1", "Suturing", 1200m },
                    { 39, "BI1", "Biopsy", 6000m },
                    { 40, "GNG1", "Gingivectomy", 2000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_medical_certificates_PatientId",
                schema: "Appointments",
                table: "medical_certificates",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "investigation_catalog",
                schema: "Appointments");

            migrationBuilder.DropTable(
                name: "medical_certificates",
                schema: "Appointments");

            migrationBuilder.DropTable(
                name: "procedure_catalog",
                schema: "Appointments");

            migrationBuilder.DropColumn(
                name: "Investigations",
                schema: "Appointments",
                table: "visits");
        }
    }
}
