using System.ComponentModel.DataAnnotations;

namespace Services.Helpers
{
    public class ValidationHelper
    {
        internal static void ModelValidation(Object obj)
        {
            ValidationContext validationContext = new(obj);
            List<ValidationResult> validationResults = [];

            //true as argument => all firlds will be validated, false as argument => only required fields will be validated
            if (!Validator.TryValidateObject(obj, validationContext, validationResults, true)) throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
        }
    }
}
