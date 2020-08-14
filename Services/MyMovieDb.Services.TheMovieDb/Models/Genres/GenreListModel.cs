using System.Collections.Generic;

namespace MyMovieDb.Services.TheMovieDb.Models.Genres
{
    public class GenreListModel
    {
        public List<GenreModel> Genres { get; set; } = new List<GenreModel>();
    }
}