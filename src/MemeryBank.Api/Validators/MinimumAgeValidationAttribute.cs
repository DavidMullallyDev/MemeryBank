using System.ComponentModel.DataAnnotations;

namespace MemeryBank.Api.Validators
{
    public class MinimumAgeValidationAttribute : ValidationAttribute
    {
        public int MinumumAge { get; set; } = 18;
        public string DefaultErrorMessage { get; set; } = "Min age = {0}";
        //parameterless constructor
        public MinimumAgeValidationAttribute()
        {
        }

        //parameterized constructor
        public MinimumAgeValidationAttribute(int minimumAge)
        {
            MinumumAge = minimumAge;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null)
            {
                DateTime date = (DateTime)value;
                if((DateTime.Now.Year - date.Year) < MinumumAge)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MinumumAge));
                } 
                else 
                {
                  return ValidationResult.Success;  
                }
            }
            return null;
        }
    }
}
