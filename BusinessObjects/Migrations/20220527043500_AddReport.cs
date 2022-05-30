using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessObjects.Migrations
{
    public partial class AddReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Users_DonatedID",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "DonatedID",
                table: "Donations",
                newName: "PostID");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_DonatedID",
                table: "Donations",
                newName: "IX_Donations_PostID");

            migrationBuilder.AddColumn<string>(
                name: "Preview",
                table: "Posts",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Bookmarks",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ReporterID = table.Column<int>(type: "int", nullable: false),
                    ReportID = table.Column<int>(type: "int", nullable: false),
                    ReportedObjectID = table.Column<int>(type: "int", nullable: false),
                    ReportType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ReportTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ReporterID);
                    table.ForeignKey(
                        name: "FK_Report_Users_ReporterID",
                        column: x => x.ReporterID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Report_ReporterID_ReportedObjectID_ReportType",
                table: "Report",
                columns: new[] { "ReporterID", "ReportedObjectID", "ReportType" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Posts_PostID",
                table: "Donations",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Posts_PostID",
                table: "Donations");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropColumn(
                name: "Preview",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Bookmarks");

            migrationBuilder.RenameColumn(
                name: "PostID",
                table: "Donations",
                newName: "DonatedID");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_PostID",
                table: "Donations",
                newName: "IX_Donations_DonatedID");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Users_DonatedID",
                table: "Donations",
                column: "DonatedID",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}
