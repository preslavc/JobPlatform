using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobPlatform.Data.Migrations
{
    public partial class AddCvMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CvMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    JobPostId = table.Column<int>(nullable: false),
                    CvUrl = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CvMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CvMessages_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CvMessages_JobPosts_JobPostId",
                        column: x => x.JobPostId,
                        principalTable: "JobPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CvMessages_ApplicationUserId",
                table: "CvMessages",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CvMessages_IsDeleted",
                table: "CvMessages",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CvMessages_JobPostId",
                table: "CvMessages",
                column: "JobPostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CvMessages");
        }
    }
}
