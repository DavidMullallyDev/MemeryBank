using MemeryBank.Api.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MemeryBank.Api.Models
{
    public class Person
    {
        public enum Gender
        {
            Male, Female, Other
        }
        [Required]
        public Guid? Id { get; set; }
        //can replace default error message with a custom error meaasage
        [Required(ErrorMessage = "{0} field is compulsory")]
        [Display(Name = "First Name")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "{0} can only contain letters")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "{0} field is compulsory")]
        [Display(Name = "Last Name")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "{0} can only contain letters")]
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        [Required(ErrorMessage = "{0} field is compulsory")]
        [Display(Name = "User Name")]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "{0} can only contain letters or numbers")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} Must be between {2} and {1} characters in length.")]
        public string? UserName { get; set; }
        [Required]
        [MinimumAgeValidationAttribute(18, ErrorMessage = "Users must be over {0} years of age!!")]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [Phone]
        public string? Phone { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        [ValidateNever] //can be used to disable validation if needed
        [Compare("Password", ErrorMessage = "{1} and {0} did not match")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }
        [Range(0, 1000, ErrorMessage = "{0} Must be between €{1} and €{2}")]
        public double? Price { get; set; }
        [Url(ErrorMessage = "url not valid")]
        public string? WebsiteUrl { get; set; }
        public DateTime? FromDate { get; set; }
        [IsValidToFromDateRangeValidationAttribute("FromDate", ErrorMessage = "'To Date' cannot be before 'From Date'")]
        public DateTime? ToDate { get; set; }
         public int Age { get; set; }
        public Gender? PersonGender { get; set; }
        public List<string?> Tags { get; set; } = [];
        public override string ToString()
        {
            string? returnStr = $"Person Object -- Id: {Id}, FirstName: {FirstName}, LastName: {LastName}, DateOfBirth: {DateOfBirth}, Password: {Password}, ConfirmPassword: {ConfirmPassword}, Price: {Price}, Username: {UserName}, FullName: {FullName}, Tags:";
            foreach (string? tag in Tags)
            {
                returnStr += tag + "|";
            }
            return returnStr;
        }
    }
}
