using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MemeryBank.Api.Validators
{
    public class IsValidToFromDateRangeValidationAttribute(string otherPropertyName) : ValidationAttribute
    {
        string OtherPropertyName { get; set; } = otherPropertyName;
        string DefaultErrorMessage { get; set; } = "To date is before from date!";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime to_date = Convert.ToDateTime(value);

                // retrieve the from date using reflection
                PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);

                if (otherProperty != null) 
                {
                    DateTime from_date = Convert.ToDateTime(otherProperty.GetValue(validationContext.ObjectInstance));

                    if (from_date > to_date)
                    {
                        //the second argument is the property being compared. if you add both you will get the error message once per proerty name. 
                        return new ValidationResult(ErrorMessage ?? DefaultErrorMessage, [OtherPropertyName, validationContext.MemberName]);
                    } else
                    {
                        return ValidationResult.Success;
                    }
                }
            }
            return null;
        }
    }
}
