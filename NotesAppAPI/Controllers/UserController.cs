using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesAppAPI.DataAccess.EF.Models;
using NotesAppAPI.DataAccess.EF.Repositories;

namespace NotesAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //GET: api/notes/{id}
        [HttpGet]
        [Authorize]
        [Route("GetUserByEmail")]
        public IActionResult GetuserById(string email)
        {
            var user = _userRepository.GetUserByEmail(email);

            try
            {
                return StatusCode(StatusCodes.Status200OK, new { messaje = "ok", response = user });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { messaje = ex.Message, response = ex });
            }
        }

        [HttpPost]
        [Route("AddUser")]
        public IActionResult CreateUser([FromBody] string userEmail, string userPassword)
        {
            User user = new User { UserEmail = userEmail };

            try
            {
               
                _userRepository.CreateUser(user, userPassword);

                return StatusCode(StatusCodes.Status200OK, new { message = "User created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = ex.Message });
            }
        }

        //PUT: api/users/{id}
        [HttpPut]
        [Authorize]
        [Route("UpdateUser")]
        public IActionResult UpdateUser(int id, [FromBody] string userPassword)
        {
            try
            {
                _userRepository.UpdatePassword(id, userPassword);
                return StatusCode(StatusCodes.Status200OK, new { messaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { messaje = ex.Message });
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("RemoveUser")]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                _userRepository.DeleteUser(userId);
                return StatusCode(StatusCodes.Status200OK, new { messaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { messaje = ex });
            }
        }
    }

    public class CreateUserRequest
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
}
