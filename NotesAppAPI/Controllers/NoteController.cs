using Microsoft.AspNetCore.Mvc;
using NotesAppAPI.DataAccess.EF.Models;
using NotesAppAPI.DataAccess.EF.Repositories;

namespace NotesAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {

        private readonly NoteRepository _noteRepository;

        public NoteController(NoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        //GET: api/notes

        [HttpGet]
        [Route("GetAllNotes")]
        public IActionResult GetAllNotes()
        {
            List<Note> list = new List<Note>();

            try
            {
                list = _noteRepository.GetAllNotes();
                return StatusCode(StatusCodes.Status200OK, new { messaje = "ok", response = list });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { messaje = ex.Message, response = list });
            }
        }

        //GET: api/notes/{id}
        [HttpGet]
        [Route("GetNoteById")]
        public IActionResult GetNoteById(int id)
        {
            var note = _noteRepository.GetNoteById(id);

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
        [Route("AddNote")]
        public IActionResult CreateNote(int userId, [FromBody] string noteTitle, string noteContent)
        {
           Note note = new Note(noteTitle, noteContent, userId);

            try
            {
                _noteRepository.CreateNote(note);
                return StatusCode(StatusCodes.Status200OK, new { messaje = note });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { messaje = ex.Message });
            }

        }

        //PUT: api/notes/{id}
        [HttpPut]
        [Route("UpdateNote")]
        public IActionResult UpdateNote(int id, [FromBody] string noteTitle, string noteContent)
        {

            try
            {
                _noteRepository.Update(id, noteTitle, noteContent);
                return StatusCode(StatusCodes.Status200OK, new { messaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new {messaje = ex.Message});
            }
        }

        [HttpDelete]
        [Route("RemoveNote")]

        public IActionResult DeleteNote(int noteId)
        {
            try
            {
                _noteRepository.Delete(noteId);
                return StatusCode(StatusCodes.Status200OK, new { messaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { messaje = ex });
            }
            

           
        }
    }
}
