using Entities;
using ServiceContracts.Enums;
namespace ServiceContracts.DTO
{
    /// <summary>
    /// Represents DTO class that is used a sreturn type of most methods of person service
    /// </summary>
    // Classes can differ as you may want to show additional information to the user on top of what ouve stored .e.g only store the Dob but you want to show the age and dob
    public class PersonResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? Dob { get; set; }
        public double Age { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool RecieveNewsletters { get; set; }

        /// <summary>
        /// Compares the current object data with the parameter object
        /// </summary>
        /// <param name="obj">The PersonResponse object to compare</param>
        /// <returns>True or False, indicating whether all person details are matched with the specified parameter object</returns>
        public override bool Equals(object? obj)
        {
            if(obj == null) return false;

            if(obj.GetType() != typeof(PersonResponse)) return false;

            PersonResponse person = (PersonResponse)obj;

            return Id == person.Id
                && Name == person.Name
                && Email == person.Email
                && Dob == person.Dob
                && Gender == person.Gender
                && CountryId == person.CountryId
                && Address == person.Address
                && RecieveNewsletters == person.RecieveNewsletters;        
        }

        public override int GetHashCode() { return HashCode.Combine(Id, Name, Email, Dob, Gender, CountryId, Address, RecieveNewsletters); }

        public override string ToString()
        {
            return $"{Id}; {Name}; {Email}; {Dob}; {Gender}; {CountryId}; {Address}; {RecieveNewsletters}";
        }
    }
}

namespace ServiceContracts.DTO
{
    public static class PersonExtensions
    {
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse()
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                Dob = person.Dob,
                Gender = person.Gender,
                CountryId = person.CountryId,
                Address = person.Address,
                RecieveNewsletters = person.RecieveNewsLetters,
                Age = (person.Dob != null)
                    ? Math.Round((DateTime.Now - person.Dob.Value).TotalDays / 365.25)
                    : 0
            };
        }

        public static PersonUpdateRequest ToPersonUpdateRequest(this Person person)
        {
            return new PersonUpdateRequest()
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                Dob = person.Dob,
                Gender = person.Gender == null? null : Enum.Parse<GenderOptions>(person.Gender, true),
                CountryId = person.CountryId,
                Address = person.Address,
                RecieveNewsLetters = person.RecieveNewsLetters,
            };
        }
    }
}
