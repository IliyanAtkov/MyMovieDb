using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyMovieDb.Data.Models;
using MyMovieDb.Services.Users.Interfaces;

namespace MyMovieDb.Services.Users.Implementations
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UsersService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<bool> IsValidUser(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return false;
            }

            return await signInManager.UserManager.CheckPasswordAsync(user, password);
        }
    }
}
