using MyMovieDb.Services.TheMovieDb.Interfaces;

namespace MyMovieDb.Services.TheMovieDb.Models.Http
{
    public class HttpServiceResult<T> where T : class, IServiceResult
    {
        public HttpServiceError? Error { get; set; }

        public T? Result { get; set; }

        public bool IsSucess { get; set; }
    }
}