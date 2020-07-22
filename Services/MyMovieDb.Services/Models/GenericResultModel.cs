namespace MyMovieDb.Services.Models
{
    public class GenericResultModel<T> : ResultModel where T : class
    {
        public T? Data { get; set; }
    }
}
