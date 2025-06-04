using System.ComponentModel.DataAnnotations;

namespace Software_Development_Assignment.Models
{
    public class AgeRangeAttribute : ValidationAttribute
    {
        private readonly int _minAge;
        private readonly int _maxAge;

        public AgeRangeAttribute(int minAge, int maxAge)
        {
            _minAge = minAge;
            _maxAge = maxAge;
            ErrorMessage = $"Age must be between {_minAge} and {_maxAge} years.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not DateTime dateOfBirth)
                return new ValidationResult("Invalid date of birth.");

            var age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-age)) age--;

            return (age >= _minAge && age <= _maxAge)
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage);
        }
    }

}
