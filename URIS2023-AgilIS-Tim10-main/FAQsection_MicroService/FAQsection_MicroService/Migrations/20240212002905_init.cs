using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FAQsection_MicroService.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FAQSections",
                columns: table => new
                {
                    FAQSectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    numberOfQuestions = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQSections", x => x.FAQSectionId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FAQSectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_FAQSections_FAQSectionId",
                        column: x => x.FAQSectionId,
                        principalTable: "FAQSections",
                        principalColumn: "FAQSectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FAQSections",
                columns: new[] { "FAQSectionId", "UserId", "numberOfQuestions" },
                values: new object[,]
                {
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), new Guid("00000000-0000-0000-0000-000000000000"), 3 },
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c1"), new Guid("00000000-0000-0000-0000-000000000000"), 2 },
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c2"), new Guid("00000000-0000-0000-0000-000000000000"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "FAQSectionId", "QuestionText" },
                values: new object[,]
                {
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c3"), new Guid("6a411c13-a195-48f7-8dbd-67596c3974c2"), "How does the software support agile project management?" },
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c4"), new Guid("6a411c13-a195-48f7-8dbd-67596c3974c1"), "Does the software tool support different agile methodologies?" }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "AnswerId", "AnswerText", "QuestionId" },
                values: new object[,]
                {
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c5"), "The software supports agile project management through features like iteration planning, task tracking, resource allocation, and transparent team communication.", new Guid("6a411c13-a195-48f7-8dbd-67596c3974c3") },
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c6"), "Yes, the software tool is flexible and can accommodate various agile methodologies, allowing users to customize it to their team's needs.", new Guid("6a411c13-a195-48f7-8dbd-67596c3974c4") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_FAQSectionId",
                table: "Questions",
                column: "FAQSectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "FAQSections");
        }
    }
}
