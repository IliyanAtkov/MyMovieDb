using System;
using System.Collections.Generic;
using System.Text;

namespace MyMovieDb.Services.Users.Interfaces
{
    public interface IUserService
    {
        bool IsValid(string username, string password);
        string[] GetUserClaims(string username);
    }
}
