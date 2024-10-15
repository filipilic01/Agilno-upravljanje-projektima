using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rag_MicroService.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rags",
                columns: table => new
                {
                    RagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RagValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BacklogItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rags", x => x.RagId);
                });

            migrationBuilder.InsertData(
                table: "Rags",
                columns: new[] { "RagId", "BacklogItemId", "RagValue" },
                values: new object[,]
                {
                    { new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f0"), new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f3"), "green" },
                    { new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f1"), new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f4"), "red" },
                    { new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f2"), new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f5"), "yellow" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rags");
        }
    }
}
