using Microsoft.EntityFrameworkCore.Migrations;

namespace JobPlatform.Data.Migrations
{
    public partial class AddFirstAndLastNameToCvMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "CvMessages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "CvMessages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "CvMessages");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "CvMessages");
        }
    }
}
