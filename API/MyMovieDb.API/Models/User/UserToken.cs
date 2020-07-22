using System.ComponentModel.DataAnnotations;

namespace MyMovieDb.API.Models.User
{
    public class UserToken
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}