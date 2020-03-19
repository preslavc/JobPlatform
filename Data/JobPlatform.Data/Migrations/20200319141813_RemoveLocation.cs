using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobPlatform.Data.Migrations
{
    public partial class RemoveLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Locations_LocationId",
                table: "Employers");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPosts_Locations_LocationId",
                table: "JobPosts");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_JobPosts_LocationId",
                table: "JobPosts");

            migrationBuilder.DropIndex(
                name: "IX_Employers_LocationId",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Employers");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "JobPosts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "JobPosts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Employers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Employers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Employers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Employers");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "JobPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Employers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPosts_LocationId",
                table: "JobPosts",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_LocationId",
                table: "Employers",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_IsDeleted",
                table: "Locations",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_Locations_LocationId",
                table: "Employers",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosts_Locations_LocationId",
                table: "JobPosts",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
