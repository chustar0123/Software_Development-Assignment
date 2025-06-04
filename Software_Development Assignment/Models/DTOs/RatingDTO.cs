using System.ComponentModel.DataAnnotations;

namespace Software_Development_Assignment.Models.DTOs
{
    public class RatingDTO
    {
        public string Question { get; set; }

        [Range(1, 5, ErrorMessage = "Please select a rating between 1 and 5.")]
        public int Rating { get; set; }
    }
}
