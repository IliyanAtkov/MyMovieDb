using MyMovieDb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieDb.Services.TheMovieDb.Interfaces
{
    public interface IMovieService : IService
    {
        Task GetNowPlaying(string language, int page = 1);
    }
}
