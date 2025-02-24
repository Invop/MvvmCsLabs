using System.ComponentModel.DataAnnotations;

namespace Lab4.Attributes;

public class BirthDateValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return new ValidationResult("Birth date is required");
        if (value is not DateTime birthDate) return new ValidationResult("Birth date is required");

        var today = DateTime.Today;

        if (birthDate > today) return new ValidationResult("Birth date cannot be in the future");

        var maxAge = 135;
        var minDate = today.AddYears(-maxAge);

        if (birthDate < minDate) return new ValidationResult($"Person cannot be older than {maxAge} years");

        return ValidationResult.Success;
    }
}