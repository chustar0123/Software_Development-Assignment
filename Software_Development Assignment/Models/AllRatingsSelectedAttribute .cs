using Software_Development_Assignment.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Software_Development_Assignment.Models
{
    public class AllRatingsSelected : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ratings = value as List<RatingDTO>;
            if (ratings == null || ratings.Count == 0)
            {
                return new ValidationResult("Ratings are required.");
            }

            if (ratings.Any(r => !r.Rating.HasValue))
            {
                return new ValidationResult("Please rate all questions by selecting one option each.");
            }

            return ValidationResult.Success;
        }
    }
}
