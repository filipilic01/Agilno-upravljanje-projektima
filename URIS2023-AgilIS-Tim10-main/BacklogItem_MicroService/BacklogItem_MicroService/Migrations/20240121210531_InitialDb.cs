using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BacklogItemMicroService.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BacklogItems",
                columns: table => new
                {
                    BacklogItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BacklogItemName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BacklogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BacklogItems", x => x.BacklogItemId);
                });

            migrationBuilder.CreateTable(
                name: "AcceptanceCriterias",
                columns: table => new
                {
                    AcceptanceCriteriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcceptanceCriteriaText = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BacklogItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcceptanceCriterias", x => x.AcceptanceCriteriaId);
                    table.ForeignKey(
                        name: "FK_AcceptanceCriterias_BacklogItems_BacklogItemId",
                        column: x => x.BacklogItemId,
                        principalTable: "BacklogItems",
                        principalColumn: "BacklogItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Descriptions",
                columns: table => new
                {
                    DescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DescriptionText = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BacklogItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descriptions", x => x.DescriptionId);
                    table.ForeignKey(
                        name: "FK_Descriptions_BacklogItems_BacklogItemId",
                        column: x => x.BacklogItemId,
                        principalTable: "BacklogItems",
                        principalColumn: "BacklogItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoryPoints",
                columns: table => new
                {
                    StoryPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoryPointValue = table.Column<int>(type: "int", nullable: false),
                    BacklogItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryPoints", x => x.StoryPointId);
                    table.ForeignKey(
                        name: "FK_StoryPoints_BacklogItems_BacklogItemId",
                        column: x => x.BacklogItemId,
                        principalTable: "BacklogItems",
                        principalColumn: "BacklogItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcceptanceCriterias_BacklogItemId",
                table: "AcceptanceCriterias",
                column: "BacklogItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Descriptions_BacklogItemId",
                table: "Descriptions",
                column: "BacklogItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoryPoints_BacklogItemId",
                table: "StoryPoints",
                column: "BacklogItemId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcceptanceCriterias");

            migrationBuilder.DropTable(
                name: "Descriptions");

            migrationBuilder.DropTable(
                name: "StoryPoints");

            migrationBuilder.DropTable(
                name: "BacklogItems");
        }
    }
}
