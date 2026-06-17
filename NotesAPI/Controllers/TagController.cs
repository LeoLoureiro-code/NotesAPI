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
    public class TagsController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

        public TagsController(
            ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        private int GetCurrentUserId()
        {
            return int.Parse(
                User.FindFirst(
                    ClaimTypes.NameIdentifier)!
                    .Value);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>>
            GetAllTags()
        {
            var tags =
                await _tagRepository
                    .GetTagsByUserAsync(
                        GetCurrentUserId());

            return Ok(tags);
        }

        [HttpGet("{tagId}")]
        public async Task<ActionResult<Tag>>
            GetTagById(int tagId)
        {
            var tag =
                await _tagRepository
                    .GetTagByIdAsync(
                        tagId,
                        GetCurrentUserId());

            if (tag == null)
            {
                return NotFound();
            }

            return Ok(tag);
        }

        [HttpPost]
        public async Task<ActionResult<Tag>>
            CreateTag(
                [FromBody]
                CreateTagDTO dto)
        {
            var userId =
                GetCurrentUserId();

            var existingTag =
                await _tagRepository
                    .GetTagByNameAsync(
                        userId,
                        dto.Name);

            if (existingTag != null)
            {
                return BadRequest(
                    new
                    {
                        message =
                        "Tag already exists."
                    });
            }

            var tag = new Tag
            {
                Name = dto.Name,
                UserId = userId
            };

            await _tagRepository
                .AddTagAsync(tag);

            await _tagRepository
                .SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTagById),
                new
                {
                    tagId = tag.TagId
                },
                tag);
        }

        [HttpDelete("{tagId}")]
        public async Task<ActionResult>
            DeleteTag(int tagId)
        {
            var existingTag =
                await _tagRepository
                    .GetTagByIdAsync(
                        tagId,
                        GetCurrentUserId());

            if (existingTag == null)
            {
                return NotFound();
            }

            await _tagRepository
                .DeleteTagAsync(
                    tagId,
                    GetCurrentUserId());

            await _tagRepository
                .SaveChangesAsync();

            return NoContent();
        }
    }
}