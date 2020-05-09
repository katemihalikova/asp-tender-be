using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asp_tender_be.Migrations
{
    public partial class AddJobApplicationAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobApplicationAnswerID",
                table: "JobApplications",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobApplicationAnswer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(maxLength: 1000, nullable: false),
                    Accepted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplicationAnswer", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobApplicationAnswerID",
                table: "JobApplications",
                column: "JobApplicationAnswerID",
                unique: true,
                filter: "[JobApplicationAnswerID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobApplicationAnswer_JobApplicationAnswerID",
                table: "JobApplications",
                column: "JobApplicationAnswerID",
                principalTable: "JobApplicationAnswer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobApplicationAnswer_JobApplicationAnswerID",
                table: "JobApplications");

            migrationBuilder.DropTable(
                name: "JobApplicationAnswer");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_JobApplicationAnswerID",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "JobApplicationAnswerID",
                table: "JobApplications");
        }
    }
}
