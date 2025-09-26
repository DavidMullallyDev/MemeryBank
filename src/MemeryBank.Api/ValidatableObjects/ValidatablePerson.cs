using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
// this can be when you want to to just have all the custom validation wriiten in the class directly 
// + simpler - the validation logic is not reusable 
namespace MemeryBank.Api.ValidatableObjects
{
    public class ValidatablePerson : IValidatableObject
    {
        public int? Age { get; set; }
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        //IEnumerable means you can return more than 1 Value --must add yield before each return 
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateOfBirth.HasValue == false && Age.HasValue == false)
            {
                // nameof(Age)...... this means that is you rename the property in the future it will also be updated here
                yield return new ValidationResult("You must suppy either your age or DOB", [nameof(Age), nameof(DateOfBirth)]);
            } else
            {
                if (DateOfBirth.HasValue)
                {
                    DateTime dateOfBirth = Convert.ToDateTime(DateOfBirth);
                    if ((DateTime.Now.Year - dateOfBirth.Year) < 18)
                    {
                        yield return new ValidationResult($"you must have been born on/before the {DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year - 18} ");
                    }
                }
                if (Age.HasValue)
                {
                    if(Age < 18)
                    {
                        yield return new ValidationResult($"you must be over 18 years of age");
                    }
                }
                if(DateOfBirth.HasValue && Age.HasValue)
                {
                    DateTime dateOfBirth = Convert.ToDateTime(DateOfBirth);
                    if ((DateTime.Now.Year - dateOfBirth.Year) != Age)
                    {
                        yield return new ValidationResult($"Your {nameof(DateOfBirth)} and {nameof(Age)} do not match");
                    }
                }
               
            }
        }

        public override string ToString()
        {
            return $"ValidatablePerson Object -- {nameof(DateOfBirth)}: {DateOfBirth}, {nameof(Age)}: {Age}";
        }
    }
}
