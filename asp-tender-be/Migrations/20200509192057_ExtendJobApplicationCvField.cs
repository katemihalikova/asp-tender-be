using Microsoft.EntityFrameworkCore.Migrations;

namespace asp_tender_be.Migrations
{
    public partial class ExtendJobApplicationCvField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cv",
                table: "JobApplications",
                newName: "CvData");

            migrationBuilder.AddColumn<string>(
                name: "CvFileName",
                table: "JobApplications",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CvMimeType",
                table: "JobApplications",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CvFileName",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "CvMimeType",
                table: "JobApplications");

            migrationBuilder.RenameColumn(
                name: "CvData",
                table: "JobApplications",
                newName: "Cv");
        }
    }
}
