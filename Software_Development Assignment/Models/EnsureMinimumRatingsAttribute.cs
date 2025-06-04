using System.ComponentModel.DataAnnotations;

namespace Software_Development_Assignment.Models
{
    public class EnsureMinimumRatingsAttribute : ValidationAttribute
    {
        private readonly int _minItems;

        public EnsureMinimumRatingsAttribute(int minItems)
        {
            _minItems = minItems;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var ratings = value as Dictionary<string, int>;
            if (ratings == null || ratings.Count < _minItems)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
