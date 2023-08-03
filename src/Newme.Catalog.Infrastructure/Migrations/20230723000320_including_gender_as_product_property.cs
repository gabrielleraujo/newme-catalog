using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Newme.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class including_gender_as_product_property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GenderId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Active", "CreateDate", "LastUpdateDate", "name" },
                values: new object[,]
                {
                    { new Guid("3309a8ad-8b90-4bc0-94f6-f1455cc11627"), true, new DateTime(2023, 7, 22, 21, 3, 20, 152, DateTimeKind.Local).AddTicks(7970), null, "não se aplica" },
                    { new Guid("440e9b4f-0488-4670-a31b-738a41740452"), true, new DateTime(2023, 7, 22, 21, 3, 20, 152, DateTimeKind.Local).AddTicks(7900), null, "feminino" },
                    { new Guid("a8277c5b-187d-4b58-9837-f808b86059dc"), true, new DateTime(2023, 7, 22, 21, 3, 20, 152, DateTimeKind.Local).AddTicks(7950), null, "masculino" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Products");
        }
    }
}
