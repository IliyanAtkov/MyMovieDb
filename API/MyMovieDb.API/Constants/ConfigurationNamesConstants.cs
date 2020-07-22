namespace MyMovieDb.API.Constants
{
    public static class ConfigurationNamesConstants
    {
        public static string JwtKey = "Jwt:Key";
        public static string JwtIssuer = "Jwt:Issuer";
        public static string JwtAuidence = "Jwt:Auidence";
        public static string JwtExpiresInMinutes = "Jwt:ExpiresInMinutes";
        public static string ConnectionString = "ConnectionString";
        public static string TheMovieDbUrl = "TheMovieDb:Url";
        public static string TheMovieDbContentTypeHeader = "TheMovieDb:ContentTypeHeader";
        public static string TheMovieDbToken = "TheMovieDb:Token";
        public static string TheMovieDbHttpClientName = "TheMovieDb:HttpClientName";
        public static string TheMovieDbPollyRetryCount = "TheMovieDb:PollyRetryCount";
        public static string TheMovieDbPollyRetryTimeInMiliseconds = "TheMovieDb:PollyRetryTimeInMiliseconds";
        public static string TheMovieDbPollyCircuitBreakerCount = "TheMovieDb:PollyCircuitBreakerCount";
        public static string TheMovieDbPollyCircuitBreakerTimeInSeconds = "TheMovieDb:PollyCircuitBreakerTimeInSeconds";
    }
}