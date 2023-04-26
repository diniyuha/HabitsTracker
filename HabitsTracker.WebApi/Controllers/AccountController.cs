using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HabitsTracker.Logic.Models;
using HabitsTracker.Logic.Services;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Получение авторизационного токена
        /// </summary>
        /// <param name="username">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        [HttpPost("token")]
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
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="email">Почта</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest("Email and password are required.");
            }

            if (_userService.CheckEmailForMatches(email))
            {
                return BadRequest("User with this email already exists.");
            }

            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                password = Encoding.UTF8.GetString(hashedBytes);
            }

            var userId = _userService.CreateUser(email, password);
            var user = _userService.GetUserById(userId);

            _userService.SendConfirmationEmailAsync(user.Email);

            return Ok();
        }

        /// <summary>
        /// Редактирование профиля пользователя
        /// </summary>
        /// <param name="user">Данные пользователя</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user)
        {
            var userEntity = _userService.GetUserByEmail(User.Identity?.Name);

            _userService.UpdateUser(userEntity.Id, user);
            return Ok();
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public IActionResult DeleteUser()
        {
            var userEmail = User.Identity?.Name;
            if (userEmail != null)
            {
                var user = _userService.GetUserByEmail(userEmail);
                if (user == null)
                {
                    return NotFound();
                }

                _userService.DeleteUser(user.Id);
                return Ok();
            }

            return NotFound();
        }
    }
}