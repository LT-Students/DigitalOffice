using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CheckRightsService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rights",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RightsHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RightId = table.Column<Guid>(nullable: false),
                    ChangedByUserId = table.Column<Guid>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RightsHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RightProjectLink",
                columns: table => new
                {
                    RightId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RightProjectLink", x => new { x.RightId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_RightProjectLink_Rights_RightId",
                        column: x => x.RightId,
                        principalTable: "Rights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RightTypeLink",
                columns: table => new
                {
                    RightId = table.Column<Guid>(nullable: false),
                    RightType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RightTypeLink", x => new { x.RightId, x.RightType });
                    table.ForeignKey(
                        name: "FK_RightTypeLink_Rights_RightId",
                        column: x => x.RightId,
                        principalTable: "Rights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RightChangeRecordTypeLink",
                columns: table => new
                {
                    RightChangeRecordId = table.Column<Guid>(nullable: false),
                    RightType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RightChangeRecordTypeLink", x => new { x.RightChangeRecordId, x.RightType });
                    table.ForeignKey(
                        name: "FK_RightChangeRecordTypeLink_RightsHistory_RightChangeRecordId",
                        column: x => x.RightChangeRecordId,
                        principalTable: "RightsHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RightRecordProjectLink",
                columns: table => new
                {
                    RightChangeRecordId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RightRecordProjectLink", x => new { x.RightChangeRecordId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_RightRecordProjectLink_RightsHistory_RightChangeRecordId",
                        column: x => x.RightChangeRecordId,
                        principalTable: "RightsHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RightChangeRecordTypeLink");

            migrationBuilder.DropTable(
                name: "RightProjectLink");

            migrationBuilder.DropTable(
                name: "RightRecordProjectLink");

            migrationBuilder.DropTable(
                name: "RightTypeLink");

            migrationBuilder.DropTable(
                name: "RightsHistory");

            migrationBuilder.DropTable(
                name: "Rights");
        }
    }
}
