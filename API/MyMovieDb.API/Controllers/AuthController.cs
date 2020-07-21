using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyMovieDb.Services.Users.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using MyMovieDb.API.Models.Auth;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace MyMovieDb.API.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IConfiguration config;

        public AuthController(IUserService userService, IConfiguration config)
        {
            this.userService = userService;
            this.config = config;
        }

        [HttpPost("token")]
        public IActionResult GenerateToken(UserAuth userAuth)
        {
            bool isValidUser = userService.IsValid(userAuth.UserName, userAuth.Password);
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
