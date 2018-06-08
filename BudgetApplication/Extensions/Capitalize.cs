using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BudgetApplication.Extensions
{
    public class Capitalize : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(validationContext.DisplayName + " is required.");
            }

            var text = value.ToString();
            if (Regex.IsMatch(text, @"^[A-Z]+[a-zA-Z''-'\s]*$"))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid text format. Word starts with upper-case and contains only letters.");
        }
    }
}
