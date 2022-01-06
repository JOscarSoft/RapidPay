using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RapidPay.Api.ApiModels;
using RapidPay.DAL;
using RapidPay.DL.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        public LoginController(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            User authUser;
            try
            {
                authUser = await _userRepository.GetByUserName(model.User);

                if (authUser == null || authUser.Password != HashPassword(model.Password))
                    return NotFound("Invalid user or password");

                var secretKey = _config.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, model.User));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(4),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                string bearer_token = tokenHandler.WriteToken(createdToken);
                return Ok(bearer_token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private static string HashPassword(string passwordToHash)
        {
            HashAlgorithm hash = new SHA256Managed();
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(passwordToHash);
            byte[] hashBytes = hash.ComputeHash(plainTextBytes);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
