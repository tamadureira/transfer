using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Transfer.Infra.Migrations
{
    public partial class Transfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryTransfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExternalID = table.Column<Guid>(type: "uniqueIdentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ExpectedOn = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryTransfers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryTransfers");
        }
    }
}
