using System.ComponentModel.DataAnnotations;

namespace Software_Development_Assignment.Models.DTOs
{
    public class SurveyDTO
    {
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [MinLength(1, ErrorMessage = "Select at least one favorite food")]
        public List<string> FavoriteFoods { get; set; }

       

        [Required]
        public List<RatingDTO> Ratings { get; set; }


    }
}
