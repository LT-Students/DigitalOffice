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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RightsHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RightsHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RightProjectLinks",
                columns: table => new
                {
                    RightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RightProjectLinks", x => new { x.RightId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_RightProjectLinks_Rights_RightId",
                        column: x => x.RightId,
                        principalTable: "Rights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RightChangeRecordProjectLink",
                columns: table => new
                {
                    RightChangeRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RightChangeRecordProjectLink", x => new { x.RightChangeRecordId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_RightChangeRecordProjectLink_RightsHistory_RightChangeRecordId",
                        column: x => x.RightChangeRecordId,
                        principalTable: "RightsHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RightChangeRecordProjectLink");

            migrationBuilder.DropTable(
                name: "RightProjectLinks");

            migrationBuilder.DropTable(
                name: "RightsHistory");

            migrationBuilder.DropTable(
                name: "Rights");
        }
    }
}
