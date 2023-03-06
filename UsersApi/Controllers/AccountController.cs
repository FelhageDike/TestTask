using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UsersApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UsersApi.DbModel;

namespace UsersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly UserDbContext _context;

        public AccountController(UserDbContext context)
        {
            _context = context;
        }
     

        [HttpPost("/token")]
        public async Task<IActionResult> Token(string Email, string password)
        {
            var identity = GetIdentity(Email, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid Email or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            var Json = JsonConvert.SerializeObject(response);
            return Ok(Json);

        }

        private ClaimsIdentity GetIdentity(string Email, string password)
        {
            password = HashPasswordHelper.GetHashedPassword(password);
            User user = this._context.Users.FirstOrDefault(x => x.Email == Email && x.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }

}
