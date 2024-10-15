using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User_Service.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.CheckConstraint("CK_User_Role", "Role IN (0, 1, 2, 3, 4)");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Name", "Password", "Role", "Surname", "Username" },
                values: new object[,]
                {
                    { new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f0"), "anaStanic@gmail.com", "Ana", "password1", 3, "Stanic", "anaS" },
                    { new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f1"), "petarP@gmail.com", "Petar", "password2", 1, "Pap", "petarP" },
                    { new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f2"), "admin@gmail.com", "Admin", "password3", 0, "Adnmin", "admin" },
                    { new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f3"), "milicaK@gmail.com", "Milica", "password4", 0, "Kokic", "milicaK" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
