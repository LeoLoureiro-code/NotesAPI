using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NotesAppAPI.DataAccess.EF.Models;
using NotesAppAPI.DataAccess.EF.Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotesAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthController(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.RefreshToken))
            {
                return BadRequest("Refresh token is required.");
            }

            var user = _userRepository.GetUserByRefreshToken(request.RefreshToken);
            if (user == null)
            {
                return Unauthorized("Invalid refresh token.");
            }

            var newAccessToken = GenerateAccessToken(user); 
            var newRefreshToken = GenerateRefreshToken(user); 

            return Ok(new { accessToken = newAccessToken, refreshToken = newRefreshToken });
        }

        private string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserEmail),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), 
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken(User user)
        {
           
            var refreshToken = Guid.NewGuid().ToString();

            
            _userRepository.UpdateRefreshToken(user.UserId, refreshToken);

            return refreshToken;
        }
    }
}
