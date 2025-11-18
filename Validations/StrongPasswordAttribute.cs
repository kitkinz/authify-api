using System.ComponentModel.DataAnnotations;

namespace AuthifyAPI.Validations;

public class StrongPasswordAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var password = value as string;
        string errorMessage;

        if (string.IsNullOrEmpty(password))
        {
            return new ValidationResult("Password is required.");
        }

        if (password.Length < 8)
        {
            errorMessage = "Password must be at least 8 characters long.";    
        }
        else if (!password.Any(char.IsUpper))
        {
            errorMessage = "Password must contain upper case";
        }
        else if (!password.Any(char.IsLower))
        {
            errorMessage = "Password must contain lower case";
        }
        else if (!password.Any(char.IsDigit))
        {
            errorMessage = "Password must contain digit";
        }
        else if (!password.Any(c => "!;@#$%^&*()".Contains(c)))
        {
            errorMessage = "Password must include one or more of these characters: '!;@#$%^&*()'";
        }
        else
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(errorMessage);
    }
}