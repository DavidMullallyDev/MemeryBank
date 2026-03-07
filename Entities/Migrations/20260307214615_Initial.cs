using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RecieveNewsLetters = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryName" },
                values: new object[,]
                {
                    { new Guid("0d14ef6e-8e55-476d-99f5-49be2e0c2fe8"), "Brazil" },
                    { new Guid("212c665b-be6f-4b44-ab0a-ef99dbafc612"), "Germany" },
                    { new Guid("7263e481-b106-4ad5-b74b-3dd232d0d761"), "Ireland" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Address", "CountryId", "Dob", "Email", "Gender", "Name", "RecieveNewsLetters" },
                values: new object[,]
                {
                    { new Guid("3201a91d-f003-4bae-8a79-d17985ef1e5c"), "HerderStr. 22, 40237 Düsseldorf", new Guid("7263e481-b106-4ad5-b74b-3dd232d0d761"), new DateTime(1985, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "mullallydavid7@gmail.com", "Male", "Dengo", true },
                    { new Guid("62ff3027-3009-44d2-9f6c-8df737472108"), "HerderStr. 22, 40237 Düsseldorf", new Guid("0d14ef6e-8e55-476d-99f5-49be2e0c2fe8"), new DateTime(1991, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "dudalice@gmail.com", "Female", "Doodles", false },
                    { new Guid("e8c5be49-f288-48d4-928a-368afbab7f7a"), "HerderStr. 22, 40237 Düsseldorf", new Guid("212c665b-be6f-4b44-ab0a-ef99dbafc612"), new DateTime(2023, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "aoife@gmail.com", "Female", "Aoifizihna", false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
