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
        [Route("GetUserById")]
        public IActionResult GetuserById(int id)
        {
            var user = _userRepository.GetUserById(id);

            try
            {
                return StatusCode(StatusCodes.Status200OK, new { messaje = "ok", response = user });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { messaje = ex.Message, response = user });
            }

        }

        [HttpPost]
        [Route("AddUser")]
        public IActionResult CreateUser([FromBody] string userEmail, string userPassword)
        {
            User user = new User(userEmail, userPassword);

            try
            {
                _userRepository.CreateUser(user);
                return StatusCode(StatusCodes.Status200OK, new { messaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { messaje = ex.Message });
            }

        }

        //PUT: api/users/{id}
        [HttpPut]
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
}
