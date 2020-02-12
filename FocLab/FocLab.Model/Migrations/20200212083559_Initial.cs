using Microsoft.EntityFrameworkCore.Migrations;

namespace FocLab.Model.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WaterCounters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentIndication = table.Column<decimal>(nullable: false),
                    FactoryNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterCounters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(maxLength: 120, nullable: true),
                    WaterCounterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Houses_WaterCounters_WaterCounterId",
                        column: x => x.WaterCounterId,
                        principalTable: "WaterCounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Houses_Address",
                table: "Houses",
                column: "Address",
                unique: true,
                filter: "[Address] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_WaterCounterId",
                table: "Houses",
                column: "WaterCounterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "WaterCounters");
        }
    }
}
