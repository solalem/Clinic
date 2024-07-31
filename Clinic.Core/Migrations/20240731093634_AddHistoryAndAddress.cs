using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Core.Migrations
{
    public partial class AddHistoryAndAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "Appointments",
                table: "visits",
                newName: "PresentIllness");

            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "Appointments",
                table: "patients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicalHistory",
                schema: "Appointments",
                table: "patients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RegisterationDate",
                schema: "Appointments",
                table: "patients",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                schema: "Appointments",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "MedicalHistory",
                schema: "Appointments",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "RegisterationDate",
                schema: "Appointments",
                table: "patients");

            migrationBuilder.RenameColumn(
                name: "PresentIllness",
                schema: "Appointments",
                table: "visits",
                newName: "Description");
        }
    }
}
