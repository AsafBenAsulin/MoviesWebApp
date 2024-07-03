using System.ComponentModel.DataAnnotations;

namespace MoviesWebApp.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        public virtual ICollection<Movie>? Movies { get; set; }
    }
}
