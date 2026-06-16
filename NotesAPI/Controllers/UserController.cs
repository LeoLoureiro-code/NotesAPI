using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesAPI.Core.Entities;
using NotesAPI.Core.Interfaces;
using NotesAPI.DTO;

namespace NotesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserController(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        [HttpGet("me")]
        public async Task<ActionResult<UserProfileDTO>> GetCurrentUser()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (email == null)
            {
                return Unauthorized();
            }

            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new UserProfileDTO
            {
                UserId = user.UserId,
                Email = user.Email,
                Role = user.Role.ToString()
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpPut("change-password")]
        public async Task<ActionResult> ChangePassword(
            [FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (email == null)
            {
                return Unauthorized();
            }

            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            var isPasswordValid =
                _passwordHasher.VerifyPassword(
                    user.HashPassword,
                    changePasswordDTO.CurrentPassword);

            if (!isPasswordValid)
            {
                return BadRequest(
                    new { message = "Current password is incorrect." });
            }

            user.HashPassword =
                _passwordHasher.HashPassword(
                    changePasswordDTO.NewPassword);

            await _userRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("me")]
        public async Task<ActionResult> DeleteMyAccount()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (email == null)
            {
                return Unauthorized();
            }

            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteUserAsync(user.UserId);

            await _userRepository.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteUserAsync(id);

            await _userRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}