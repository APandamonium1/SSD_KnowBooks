using System.ComponentModel.DataAnnotations;

namespace KnowBooks.Models
{
    public class Review
    {
        [Key] //Sets ISBN as primary key with PK constraints
        public int ISBN { get; set; }

        public string BookTitle { get; set; } = string.Empty;

        public string User { get; set; } = string.Empty;

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [StringLength(100, MinimumLength = 1)]
        public string Text { get; set; } = string.Empty;
    }
}
