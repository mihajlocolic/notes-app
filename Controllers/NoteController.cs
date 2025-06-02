using Microsoft.AspNetCore.Mvc;
using NotesAPI.Models;
using NotesAPI.Handlers;

namespace NotesAPI.Controllers
{
    // Very important to specify these two when creating controllers manually.
    [ApiController]
    [Route("notes")]
    public class NoteController : Controller
    {
        NotesDataHandler _notesDataHandler = new NotesDataHandler();

        [HttpGet]
        [Route("/notes")]
        public IActionResult GetAllNotes()
        {
            List<Note> notes = _notesDataHandler.ReadNotesFromJson();

            if (notes.Count == 0) return NoContent();
            return Ok(notes);
        }

        [HttpGet]
        [Route("/notes/{id}")]
        public IActionResult GetNote(int id)
        {
            List<Note> notes = _notesDataHandler.ReadNotesFromJson();
            try
            {
                Note? note = notes.Find(n => n.Id == id);
                if (note != null)
                {
                    return Ok(note);
                }
                return NotFound(new NullReferenceException($"Note with id {id} doesn't exist!"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        [HttpPost]
        [Route("/notes")]
        public IActionResult CreateNote(Note? note)
        {
            try {
                List<Note> notes = _notesDataHandler.ReadNotesFromJson();
                if (note == null) return BadRequest(new NullReferenceException("Note object was null."));
                notes.Add(note);
                _notesDataHandler.WriteNotesToJson(notes);
                return Ok("New note created.");

            } catch (Exception exc) {
                Console.WriteLine(exc.Message);
                throw;
            }
            
        }

        [HttpPut]
        [Route("/notes/{id}")]
        public IActionResult UpdateNote(int id, [FromBody] Note note)
        {
            try {

                if (id < 1) return BadRequest(new Exception("Id must be positive number."));
                
                List<Note> notes = _notesDataHandler.ReadNotesFromJson();
                if (notes.Find(n => n.Id == note.Id ) == null) return BadRequest(new Exception("Note not found."));

                int noteIdx = notes.FindIndex(x => x.Id == id);
                notes[noteIdx].Title = note.Title;
                notes[noteIdx].Content = note.Content;
                notes[noteIdx].CreatedAt = note.CreatedAt;

                _notesDataHandler.WriteNotesToJson(notes);

                return Ok("Note updated.");
                

            } catch (Exception exc) {
                Console.WriteLine(exc.Message);
                throw;
            }

            
        }

        [HttpDelete]
        [Route("/notes/{id}")]
        public IActionResult DeleteNote(int id)
        {
            try {
                if (id < 1) return BadRequest(new Exception("Id must be positive number."));
                List<Note> notes = _notesDataHandler.ReadNotesFromJson();
                int noteIdx = notes.FindIndex(x => x.Id == id);
                if (noteIdx.Equals(0)) return BadRequest(new Exception("Note not found."));
                notes.RemoveAt(noteIdx);
                _notesDataHandler.WriteNotesToJson(notes);
                return Ok("Note deleted.");

            } catch (Exception exc) {
                Console.WriteLine(exc.Message);
                throw;
            }
            
        }

    }
}

