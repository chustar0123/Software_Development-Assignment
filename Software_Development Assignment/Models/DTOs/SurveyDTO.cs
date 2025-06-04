using System.ComponentModel.DataAnnotations;

namespace Software_Development_Assignment.Models.DTOs
{
    public class SurveyDTO 
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact number must be a 10-digit number.")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [AgeRange(5, 120)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please select at least one favorite food.")]
        public List<string> FavoriteFoods { get; set; } = new();

        [AllRatingsSelected(ErrorMessage = "Please rate all questions by selecting one option each.")]
        public List<RatingDTO> Ratings { get; set; } = new();




    }
}
