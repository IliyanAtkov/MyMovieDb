using MyMovieDb.Services.Interfaces;
using System.Threading.Tasks;

namespace MyMovieDb.Services.Users.Interfaces
{
    public interface IUsersService : IService
    {
        Task<bool> IsValidUser(string username, string password);
    }
}