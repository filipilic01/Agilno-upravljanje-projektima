using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Status_MicroService.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    IdStatusa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VrednostStatusa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BacklogItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.IdStatusa);
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "IdStatusa", "BacklogItemId", "VrednostStatusa" },
                values: new object[,]
                {
                    { new Guid("4e4bbba9-9dbe-4f8d-847a-310e08d26de5"), new Guid("4e4bbba9-9dbe-4f8d-847a-310e08d26de2"), "Done" },
                    { new Guid("b293a04a-2900-48b4-9b1c-a6f3455aa8d5"), new Guid("4e4bbba9-9dbe-4f8d-847a-310e08d26de9"), "Done" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Status_BacklogItemId",
                table: "Status",
                column: "BacklogItemId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
