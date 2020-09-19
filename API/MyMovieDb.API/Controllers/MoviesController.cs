using Microsoft.AspNetCore.Mvc;
using MyMovieDb.Services.TheMovieDb.Interfaces;
using MyMovieDb.Services.TheMovieDb.Models.Movies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMovieDb.API.Controllers
{
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public async Task<IEnumerable<MoviesListModel>> GetNowPlaying(string language)
        {
            var result = await movieService.GetNowPlaying(language);
            return result;
        }
    }
}
