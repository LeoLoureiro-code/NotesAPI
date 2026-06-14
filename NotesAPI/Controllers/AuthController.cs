using Microsoft.AspNetCore.Mvc;
using NotesAPI.Core.Entities;
using NotesAPI.Core.Enums;
using NotesAPI.Core.Interfaces;
using NotesAPI.DTO;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public AuthController(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDTO>> Register(
        [FromBody] RegisterRequestDTO registerRequest)
    {
        var existingUser = await _userRepository
            .GetUserByEmailAsync(registerRequest.Email);

        if (existingUser != null)
        {
            return Conflict("Email already exists.");
        }

        var user = new User
        {
            Email = registerRequest.Email,
            HashPassword = _passwordHasher
                .HashPassword(registerRequest.Password),
            Role = Role.User
        };

        await _userRepository.CreateUserAsync(user);

        await _userRepository.SaveChangesAsync();

        var token = _jwtService.GenerateToken(user);

        return Ok(new AuthResponseDTO
        {
            Token = token,
            Email = user.Email,
            Role = user.Role.ToString()
        });
    }

        [HttpPost("login")]
        public async Task<ActionResult<LoginRequestDTO>> Login(
            [FromBody] LoginRequestDTO loginRequest)
        {
            var user = await _userRepository
                .GetUserByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            var isPasswordValid = _passwordHasher
                .VerifyPassword(user.HashPassword, loginRequest.Password);
            if (!isPasswordValid)
            {
                return Unauthorized("Invalid email or password.");
            }
            var token = _jwtService.GenerateToken(user);
            return Ok(new AuthResponseDTO
            {
                Token = token,
                Email = user.Email,
                Role = user.Role.ToString()
            });
    }
}