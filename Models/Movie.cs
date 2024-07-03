using System.ComponentModel.DataAnnotations;

namespace MoviesWebApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Title field is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 100 characters.")]
        public string Title { get; set; }

        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10.")]
        public int Rating { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int CategoryId2 { get; set; }
        public virtual Category Category2 { get; set; }
    }
}
