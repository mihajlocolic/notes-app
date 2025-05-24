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
        NotesDataHandler notesDataHandler = new NotesDataHandler();

        [HttpGet]
        public List<Note> GetAllNotes()
        {
            return notesDataHandler.ReadNotesFromJson();
        }

        [HttpGet]
        [Route("/notes/{id}")]
        public Note GetNote(int id)
        {
            List<Note> notes = notesDataHandler.ReadNotesFromJson();
            Note? note = notes.Find(n => n.Id == id);

            if (note != null)
            {
                return note;
            }
            else
            {
                throw new NullReferenceException($"Searched note id {id} was null.");
            }
        }

        [HttpPost("/notes")]
        public string CreateNote(Note note)
        {
            List<Note> notes = notesDataHandler.ReadNotesFromJson();
            notes.Add(note);
            notesDataHandler.WriteNotesToJson(notes);
            return "New note created.";
        }

        [HttpPut("/{id}")]
        public string UpdateNote()
        {
            return "Should update the note";
        }

        [HttpDelete("/{id}")]
        public string DeleteNote()
        {
            return "Should delete the note";
        }

    }
}

