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

        [HttpPut]
        [Route("/notes/{id}")]
        public string UpdateNote(int id, [FromBody] Note note)
        {
            if (id > 0)
            {
                List<Note> notes = notesDataHandler.ReadNotesFromJson();
                int noteIdx = notes.FindIndex(x => x.Id == id);
                notes[noteIdx].Title = note.Title;
                notes[noteIdx].Content = note.Content;
                notes[noteIdx].CreatedAt = note.CreatedAt;

                notesDataHandler.WriteNotesToJson(notes);

                return "Note updated";
            }

            return "Id must be positive number";
        }

        [HttpDelete("/notes/{id}")]
        public string DeleteNote(int id)
        {
            List<Note> notes = notesDataHandler.ReadNotesFromJson();
            int noteIdx = notes.FindIndex(x => x.Id == id);

            notes.RemoveAt(noteIdx);

            notesDataHandler.WriteNotesToJson(notes);

            return "Note deleted.";
        }

    }
}

