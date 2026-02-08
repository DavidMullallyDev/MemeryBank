using Entities;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    public class PersonDeleteRequest
    {
        [Required(ErrorMessage = "Id cannot be blank")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Required field")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Required field")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string? Email { get; set; }
        public DateTime? Dob { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Address { get; set; }
        public bool RecieveNewsletters { get; set; }


        /// <summary>
        /// Converts the current object of PersonAddRequest into a new object of Person type
        /// </summary>
        /// <returns></returns>
        public Person ToPerson()
        {
            return new Person()
            {
                Id = Id,
                Name = Name,
                Email = Email,
                Dob = Dob,
                Gender = Gender.ToString(),
                CountryId = CountryId,
                Address = Address,
                RecieveNewsletters = RecieveNewsletters
            };
        }
    }
}
