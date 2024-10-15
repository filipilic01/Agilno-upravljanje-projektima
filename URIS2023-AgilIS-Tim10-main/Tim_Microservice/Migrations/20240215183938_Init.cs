using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tim_Microservice.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tim",
                columns: table => new
                {
                    TimID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivTima = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojClanova = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tim", x => x.TimID);
                });

            migrationBuilder.CreateTable(
                name: "ClanTima",
                columns: table => new
                {
                    ClanTimaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClanTima", x => x.ClanTimaId);
                    table.ForeignKey(
                        name: "FK_ClanTima_Tim_TimId",
                        column: x => x.TimId,
                        principalTable: "Tim",
                        principalColumn: "TimID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClanTima_TimId",
                table: "ClanTima",
                column: "TimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClanTima");

            migrationBuilder.DropTable(
                name: "Tim");
        }
    }
}
