using Microsoft.AspNetCore.Mvc;
using MyMovieDb.Services.TheMovieDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovieDb.API.Controllers
{
    public class TestController : ControllerBase
    {
        private readonly IMovieService movieService;

        public TestController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            await movieService.GetNowPlaying("en");
            return new EmptyResult();
        }
    }
}
