using System.ComponentModel.DataAnnotations;

namespace Lab4.Attributes;

public class BirthDateValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return new ValidationResult("Birth date is required");
        if (value is not DateTime birthDate) return new ValidationResult("Birth date is required");

        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age))
            age--;
        return age switch
        {
            < 0 => new ValidationResult("Дата народження не може бути в майбутньому"),
            > 135 => new ValidationResult("Вік не може бути більше 135 років"),
            _ => ValidationResult.Success
        };
    }
}