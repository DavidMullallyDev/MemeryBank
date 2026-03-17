using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Entities
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Person>().ToTable("Persons");

            string countriesJson = File.ReadAllText(@"SeedData\countries.json");
            List<Country> countries = JsonSerializer.Deserialize<List<Country>>(countriesJson) ?? [];

            foreach (Country country in countries)
            {
                modelBuilder.Entity<Country>().HasData(country);
            }

            string personsJson = File.ReadAllText(@"SeedData\persons.json");
            List<Person> persons = JsonSerializer.Deserialize<List<Person>>(personsJson) ?? [];

            foreach (Person person in persons)
            {
                modelBuilder.Entity<Person>().HasData(person);
            }

            //Fluent API
            modelBuilder.Entity<Person>().Property(p => p.TIN)
                .HasColumnName("TaxIdentificationNumber")
                .HasColumnType("varchar(8)");
            //.HasDefaultValue("12345678");

            //modelBuilder.Entity<Person>().HasIndex(p => p.TIN).IsUnique(); //index allows for faster searching
            modelBuilder.Entity<Person>().HasCheckConstraint("CHK_TIN", "len([TaxIdentificationNumber]) = 8"); //TIN must be exactly 8 characters long
        }

        public List<Person> SP_GetPersonsList()
        {
            return [.. Persons.FromSqlRaw("EXECUTE [dbo].[GetPersonsList]")];
        }

        public int SP_AddPerson(Person person)
        {
            SqlParameter[] parameters =
            [
               new SqlParameter("Id", person.Id),
               new SqlParameter("@Name", person.Name ?? (object)DBNull.Value),
               new SqlParameter("@Email", person.Email ?? (object)DBNull.Value),
               new SqlParameter("@Dob", person.Dob ?? (object)DBNull.Value),
               new SqlParameter("@Gender", person.Gender ?? (object)DBNull.Value),
               new SqlParameter("@CountryId", person.CountryId ?? (object)DBNull.Value),
               new SqlParameter("@Address", person.Address ?? (object)DBNull.Value),
               new SqlParameter("@RecieveNewsLetters", person.RecieveNewsLetters)
            ];

            return Database.ExecuteSqlRaw("EXECUTE [dbo].[AddPerson] @Id, @Name, @Email, @Dob, @Gender, @CountryId, @Address, @RecieveNewsLetters", parameters);
        }
    }
}
