using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Extensions.Configuration;
// using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using OnlyFundsAPI.API.Models;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;
using OnlyFundsAPI.Utilities;

namespace OnlyFundsAPI.API.Controllers
{
    [ApiController]
    [Route("odata/authentication")]
    public class JWTController : ControllerBase
    {
        public IConfiguration configuration;
        private readonly IRepoWrapper repo;
        public JWTController(IConfiguration configuration, IRepoWrapper repo)
        {
            this.configuration = configuration;
            this.repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //check if user is admin
            string adminUsername = configuration["Admin:Username"];
            string adminPassword = configuration["Admin:Password"];
            bool isAdmin = false;
            User user = null;
            if (loginModel.Username.Equals(adminUsername) && PasswordUtils.HashString(loginModel.Password).Equals(adminPassword))
            {
                isAdmin = true;
                user = new User
                {
                    UserID = Int32.Parse(configuration["Admin:UserId"]),
                    Username = loginModel.Username,
                    Email = configuration["Admin:Email"]
                };
            }
            else
            {
                user = await repo.Users.GetUserByUsernameAndPassword(loginModel.Username, loginModel.Password);
            }

            if (user == null) return Unauthorized("Incorrect username or password");

            //set role
            string role = "";
            if (isAdmin) role = "Admin";
            else role = "User";

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserId", user.UserID.ToString()),
                new Claim("Email", user.Email),
                new Claim("Username", user.Username),
                new Claim(ClaimTypes.Role, role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime expirationTime;
            if (loginModel.RememberMe) expirationTime = DateTime.UtcNow.AddDays(30);
            else expirationTime = DateTime.UtcNow.AddMinutes(30);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: expirationTime,
                signingCredentials: signIn
            );
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}