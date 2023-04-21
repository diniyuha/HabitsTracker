using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HabitsTracker.Logic.Models;
using HabitsTracker.Logic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HabitsTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("/token")]
        public IActionResult Token(string username, string password)
        {
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new {errorText = "Invalid username or password."});
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(int.Parse(_configuration["Jwt:Lifetime"]))),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                    SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Json(response);
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            User user = _userService.GetAuthUser(username, password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "user")
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}