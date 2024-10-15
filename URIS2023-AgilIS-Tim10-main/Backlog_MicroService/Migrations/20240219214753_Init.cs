using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backlog_MicroService.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Backlogs",
                columns: table => new
                {
                    IdBacklog = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KorisnikId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProjekatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpisZadatak = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VremeTrajanja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AngazovaniRadnici = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pocetak = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Kraj = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cilj = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backlogs", x => x.IdBacklog);
                });

            migrationBuilder.InsertData(
                table: "Backlogs",
                columns: new[] { "IdBacklog", "Cilj", "Discriminator", "KorisnikId", "Kraj", "Naslov", "Opis", "Pocetak", "ProjekatId" },
                values: new object[] { new Guid("4e4bbba9-9dbe-4f8d-847a-310e08d26de5"), "Cilj SprintBackloga", "SprintBacklog", new Guid("1c989ee3-13b2-4d3b-abeb-c4e6343eace1"), new DateTime(2024, 2, 26, 22, 47, 53, 54, DateTimeKind.Local).AddTicks(5437), "Novi micro servis", "Napraviti micro servis za baclog", new DateTime(2024, 2, 19, 22, 47, 53, 54, DateTimeKind.Local).AddTicks(5390), new Guid("7c259ee3-13b2-4d3b-abeb-c4e6343eace1") });

            migrationBuilder.InsertData(
                table: "Backlogs",
                columns: new[] { "IdBacklog", "AngazovaniRadnici", "Discriminator", "KorisnikId", "Naslov", "Opis", "OpisZadatak", "ProjekatId", "VremeTrajanja" },
                values: new object[] { new Guid("7c55d20c-a4cf-4c47-b44b-af8b58101b42"), "Angažovani radnici ProductBackloga", "ProductBacklog", new Guid("7aae8c74-674b-4049-8c7f-9e4f0e5c6109"), "Naslov ProductBackloga", "Opis ProductBackloga", "Opis zadatka ProductBackloga", new Guid("7c259ee3-13b2-4d3b-abeb-c4e6343eace1"), "Vreme trajanja ProductBackloga" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Backlogs");
        }
    }
}
