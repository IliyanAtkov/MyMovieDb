namespace MyMovieDb.Services.TheMovieDb.Models.Companies
{
    public class CompanyModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? LogoPath { get; set; }

        public string? OriginCountry { get; set; }
    }
}