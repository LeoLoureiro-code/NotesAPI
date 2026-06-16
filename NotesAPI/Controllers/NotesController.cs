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
    public class NotesController : ControllerBase
    {
        private readonly INotesRepository _notesRepository;

        public NotesController(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        private int GetCurrentUserId()
        {
            return int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!
                    .Value);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetAllNotes()
        {
            var userId = GetCurrentUserId();

            var notes = await _notesRepository
                .GetNotesByUserAsync(userId);

            return Ok(notes);
        }

        [HttpGet("{noteId}")]
        public async Task<ActionResult<Note>> GetNoteById(int noteId)
        {
            var userId = GetCurrentUserId();

            var note = await _notesRepository
                .GetNoteByIdAsync(noteId, userId);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPost]
        public async Task<ActionResult<Note>> CreateNote(
            [FromBody] NotesInformationDTO notesInformationDTO)
        {
            var userId = GetCurrentUserId();

            var note = new Note
            {
                Title = notesInformationDTO.Title,
                Content = notesInformationDTO.Content,
                IsArchived = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = userId
            };

            await _notesRepository.CreateNoteAsync(note);

            await _notesRepository.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetNoteById),
                new { noteId = note.NoteId },
                note);
        }

        [HttpPut("{noteId}")]
        public async Task<ActionResult> UpdateNote(
            int noteId,
            [FromBody] NotesInformationDTO notesInformationDTO)
        {
            var userId = GetCurrentUserId();

            var existingNote = await _notesRepository
                .GetNoteByIdAsync(noteId, userId);

            if (existingNote == null)
            {
                return NotFound();
            }

            existingNote.Title = notesInformationDTO.Title;
            existingNote.Content = notesInformationDTO.Content;
            existingNote.IsArchived = notesInformationDTO.IsArchived;
            existingNote.UpdatedAt = DateTime.UtcNow;

            await _notesRepository.UpdateNoteAsync(existingNote);

            await _notesRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{noteId}")]
        public async Task<ActionResult> DeleteNote(int noteId)
        {
            var userId = GetCurrentUserId();

            var existingNote = await _notesRepository
                .GetNoteByIdAsync(noteId, userId);

            if (existingNote == null)
            {
                return NotFound();
            }

            await _notesRepository
                .DeleteNoteAsync(noteId, userId);

            await _notesRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("archived")]
        public async Task<ActionResult<IEnumerable<Note>>> GetArchivedNotes()
        {
            var userId = GetCurrentUserId();

            var archivedNotes = await _notesRepository
                .GetArchivedNotesAsync(userId);

            return Ok(archivedNotes);
        }

        [HttpPatch("{noteId}/archive")]
        public async Task<ActionResult> ArchiveNote(int noteId)
        {
            var userId = GetCurrentUserId();

            var existingNote = await _notesRepository
                .GetNoteByIdAsync(noteId, userId);

            if (existingNote == null)
            {
                return NotFound();
            }

            existingNote.IsArchived = true;
            existingNote.UpdatedAt = DateTime.UtcNow;

            await _notesRepository
                .UpdateNoteAsync(existingNote);

            await _notesRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{noteId}/unarchive")]
        public async Task<ActionResult> UnarchiveNote(int noteId)
        {
            var userId = GetCurrentUserId();

            var existingNote = await _notesRepository
                .GetNoteByIdAsync(noteId, userId);

            if (existingNote == null)
            {
                return NotFound();
            }

            existingNote.IsArchived = false;
            existingNote.UpdatedAt = DateTime.UtcNow;

            await _notesRepository
                .UpdateNoteAsync(existingNote);

            await _notesRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("tags/{tagId}")]
        public async Task<ActionResult<IEnumerable<Note>>>
            GetNotesByTag(int tagId)
        {
            var userId = GetCurrentUserId();

            var notes = await _notesRepository
                .GetNotesByTagAsync(userId, tagId);

            return Ok(notes);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Note>>>
            SearchNotes([FromQuery] string query)
        {
            var userId = GetCurrentUserId();

            var notes = await _notesRepository
                .SearchNotesAsync(userId, query);

            return Ok(notes);
        }
    }
}