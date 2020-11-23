using System.ComponentModel.DataAnnotations;

namespace MyMovieDb.API.Models.Movies
{
    public class MoviesListInputModel
    {
        [Required]
        public string Language { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int Page { get; set; }
    }
}