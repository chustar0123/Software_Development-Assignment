using System.ComponentModel.DataAnnotations;

namespace Software_Development_Assignment.Models.DTOs
{
    public class RatingDTO
    {
        public string Question { get; set; }

        public int? Rating { get; set; }
    }
}
