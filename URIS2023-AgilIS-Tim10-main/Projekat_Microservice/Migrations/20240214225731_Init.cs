using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekat_Microservice.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projekat",
                columns: table => new
                {
                    ProjekatID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivProjekta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpisProjekta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumProjekta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekat", x => x.ProjekatID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projekat");
        }
    }
}
