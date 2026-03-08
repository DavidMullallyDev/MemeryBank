using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class GetPersons_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_getPersonnsList = @"
                CREATE PROCEDURE [dbo].[GetPersonsList]
                AS BEGIN
                SELECT ID, Name, Email, Dob, Gender, CountryId, Address, RecieveNewsLetters
                FROM [dbo].[Persons]
                END
                ";
            migrationBuilder.Sql(sp_getPersonnsList);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_getPersonnsList = @"
                DROP PROCEDURE [dbo].[GetPersonsList]
                ";
            migrationBuilder.Sql(sp_getPersonnsList);
        }
    }
}
