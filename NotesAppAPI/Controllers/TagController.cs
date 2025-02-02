using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesAppAPI.DataAccess.EF.Models;
using NotesAppAPI.DataAccess.EF.Repositories;

namespace NotesAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TagController : ControllerBase
    {

        private readonly TagRepository _tagRepository;

        public TagController(TagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        //GET: api/notes

        [HttpGet]
        [Route("GetAllTags")]
        public IActionResult GetAllTags()
        {
            List<Tag> list = new List<Tag>();

            try
            {
                list = _tagRepository.GetAllTags();
                return StatusCode(StatusCodes.Status200OK, new { messaje = "ok", response = list });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { messaje = ex.Message, response = list });
            }
        }

        //GET: api/tags/{id}
        [HttpGet]
        [Route("GetTagById")]
        public IActionResult GetTagById(int id)
        {
            var note = _tagRepository.GetTagById(id);

            try
            {
                return StatusCode(StatusCodes.Status200OK, new { messaje = "ok", response = note });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { messaje = ex.Message, response = note });
            }

        }

        [HttpPost]
        [Route("AddTag")]
        public IActionResult CreateTag([FromBody] string tagName)
        {
            Tag tag = new Tag(tagName);

            try
            {
                _tagRepository.CreateTag(tag);
                return StatusCode(StatusCodes.Status200OK, new { messaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { messaje = ex.Message });
            }

        }

        //PUT: api/tags/{id}
        [HttpPut]
        [Route("UpdateTag")]
        public IActionResult UpdateTag(int tagId, [FromBody] string tagName)
        {

            try
            {
                _tagRepository.UpdateTag(tagId, tagName);
                return StatusCode(StatusCodes.Status200OK, new { messaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { messaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("RemoveTag")]

        public IActionResult DeleteTag(int tagId)
        {
            try
            {
                _tagRepository.DeleteTag(tagId);
                return StatusCode(StatusCodes.Status200OK, new { messaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { messaje = ex });
            }



        }
    }
}
