using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Testeger.Client.Attributes;

public class UsernameValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string strValue)
        {
            var regex = new Regex(@"^[a-zA-Z0-9_]+$");

            if (strValue.Contains(" "))
            {
                return new ValidationResult("The username cannot contain spaces.");
            }
            else if (!regex.IsMatch(strValue))
            {
                return new ValidationResult("The username can only contain letters, numbers, and underscores.");
            }
        }

        return ValidationResult.Success;
    }
}
