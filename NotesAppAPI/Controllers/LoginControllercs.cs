using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NotesAppAPI.DataAccess.EF.Models;
using NotesAppAPI.DataAccess.EF.Repositories;
using NotesAppAPI.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public LoginController(UserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        var user = _userRepository.GetUserByEmail(loginRequest.UserEmail);

        if (user == null || !VerifyPassword(user, loginRequest.UserPassword))
        {
            return Unauthorized("Invalid credentials");
        }

        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken(user);

        // Optionally save the refresh token in the database if needed
        _userRepository.UpdateRefreshToken(user.UserId, refreshToken);

        return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
    }

    private bool VerifyPassword(User user, string inputPassword)
    {
        try
        {
            var passwordHasher = new PasswordHasher<User>();


            var result = passwordHasher.VerifyHashedPassword(user, user.UserPassword, inputPassword);


            return result == PasswordVerificationResult.Success;
        }
        catch (FormatException ex)
        {
            return false;
        }
    }


    private string GenerateAccessToken(User user)
    {
        try
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserEmail),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToInt32(jwtSettings["AccessTokenExpiration"]));

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error generating token: " + ex.Message);
            throw;
        }
    }

    private string GenerateRefreshToken(User user)
    {
        return Guid.NewGuid().ToString(); 
    }
}
