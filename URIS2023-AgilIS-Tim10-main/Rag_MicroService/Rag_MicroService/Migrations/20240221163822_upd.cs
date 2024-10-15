using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rag_MicroService.Migrations
{
    /// <inheritdoc />
    public partial class upd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rags_BacklogItemId",
                table: "Rags",
                column: "BacklogItemId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rags_BacklogItemId",
                table: "Rags");
        }
    }
}
