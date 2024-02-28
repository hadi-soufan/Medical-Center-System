using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MedicalHistoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalHistories",
                columns: table => new
                {
                    MedicalHistoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Height = table.Column<decimal>(type: "numeric", nullable: false),
                    Weight = table.Column<decimal>(type: "numeric", nullable: false),
                    MedicalProblems = table.Column<string>(type: "text", nullable: true),
                    MentalHealthProblems = table.Column<string>(type: "text", nullable: true),
                    Allergics = table.Column<string>(type: "text", nullable: true),
                    SugreriesHistory = table.Column<string>(type: "text", nullable: true),
                    Vaccines = table.Column<string>(type: "text", nullable: true),
                    Diagnosis = table.Column<string>(type: "text", nullable: true),
                    TestsPerformed = table.Column<string>(type: "text", nullable: true),
                    TreatmenPlans = table.Column<string>(type: "text", nullable: true),
                    FamilyMedicalHistory = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistories", x => x.MedicalHistoryId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalHistories");
        }
    }
}
