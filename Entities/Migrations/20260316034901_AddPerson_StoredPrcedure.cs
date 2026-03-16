using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddPerson_StoredPrcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_AddPerson = @"
                CREATE PROCEDURE [dbo].[AddPerson] (@Id uniqueidentifier, @Name nvarchar(50), @Email nvarchar(50), @Dob datetime, @Gender nvarchar(10), @CountryId uniqueidentifier, @Address nvarchar(500), @RecieveNewsLetters bit)
                AS BEGIN
                INSERT INTO [dbo].[Persons] (Id, Name, Email, Dob, Gender, CountryId, Address, RecieveNewsLetters) VALUES (@Id, @Name, @Email, @Dob, @Gender, @CountryId, @Address, @RecieveNewsLetters)
                END
                ";
            migrationBuilder.Sql(sp_AddPerson);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_AddPerson = @"
                DROP PROCEDURE [dbo].[AddPerson]
                ";
            migrationBuilder.Sql(sp_AddPerson);
        }
    }
}
