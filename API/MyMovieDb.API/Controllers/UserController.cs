using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyMovieDb.Services.Users.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Threading.Tasks;
using MyMovieDb.API.Models.User;

namespace MyMovieDb.API.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUsersService userService;
        private readonly IConfiguration config;

        public UserController(IUsersService userService, IConfiguration config)
        {
            this.userService = userService;
            this.config = config;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken(UserToken userToken)
        {
            bool isValidUser = await userService.IsValidUser(userToken.UserName, userToken.Password);
            if (!isValidUser)
            {
                return BadRequest("invalid user/pass combination");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Auidence"],
                expires: DateTime.Now.AddMinutes(double.Parse(config["Jwt:ExpiresInMinutes"])),
                signingCredentials: creds
                );

            return Ok(
                new 
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
        }
    }
}
