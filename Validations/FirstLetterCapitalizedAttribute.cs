using System.ComponentModel.DataAnnotations;

namespace APICatalog.Validations;

public class FirstLetterCapitalizedAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value,
        ValidationContext validationContext)
    {
        var strValue = value?.ToString();
        if (string.IsNullOrEmpty(strValue))
        {
            return ValidationResult.Success!;
        }

        var firstLetter = strValue[0].ToString();
        if (firstLetter != firstLetter?.ToUpper())
        {
            return new ValidationResult(
                "The first letter of the name must be capitalized."
            );
        }

        return ValidationResult.Success!;
    }
}