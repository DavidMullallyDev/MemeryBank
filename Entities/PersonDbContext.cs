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
        }
    }
}
