using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.DigitalOffice.CheckRightsService.Database.Migrations
{
    public partial class RenamedTableRightUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RightsUsers",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RightId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RightsUsers", x => new { x.UserId, x.RightId });
                    table.ForeignKey(
                        name: "FK_RightsUsers_Rights_RightId",
                        column: x => x.RightId,
                        principalTable: "Rights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RightsUsers_RightId",
                table: "RightsUsers",
                column: "RightId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RightsUsers");
        }
    }
}
