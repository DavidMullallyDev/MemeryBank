using Entities;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Acts as a DTO for adding a new person
    /// </summary>
    public class PersonAddRequest
    {
        [Required(ErrorMessage = "Name Is A Required field")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email Is A Required field")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
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
