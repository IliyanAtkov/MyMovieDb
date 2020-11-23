using Microsoft.AspNetCore.Mvc;
using MyMovieDb.API.Attributes;
using MyMovieDb.API.Models.Movies;
using MyMovieDb.Services.TheMovieDb.Interfaces;
using MyMovieDb.Services.TheMovieDb.Models.Movies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMovieDb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public async Task<IEnumerable<MoviesListModel>> GetNowPlaying(MoviesListInputModel model)
        {
            var nowPlayingMovies = await movieService.GetNowPlaying(model.Language, model.Page);
            return nowPlayingMovies;
        }

        public async Task<IEnumerable<MoviesListModel>> GetPopular(MoviesListInputModel model)
        {
            var popularMovies = await movieService.GetPopular(model.Language, model.Page);
            return popularMovies;
        }
    }
}
