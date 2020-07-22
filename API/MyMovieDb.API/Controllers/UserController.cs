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
using MyMovieDb.API.Constants;

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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config[ConfigurationNamesConstants.JwtKey]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: config[ConfigurationNamesConstants.JwtIssuer],
                audience: config[ConfigurationNamesConstants.JwtAuidence],
                expires: DateTime.Now.AddMinutes(double.Parse(config[ConfigurationNamesConstants.JwtExpiresInMinutes])),
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
