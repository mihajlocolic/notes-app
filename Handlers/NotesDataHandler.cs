
using NotesAPI.Models;
using System.Text.Json;

namespace NotesAPI.Handlers
{
    public class NotesDataHandler
    {
        private string JsonPath = "notes.json";


        public List<Note> ReadNotesFromJson()
        {
            try
            {
                NotesFileCheck(JsonPath);
                StreamReader sr = new StreamReader(JsonPath);
                string json = sr.ReadToEnd();
                List<Note>? notes = JsonSerializer.Deserialize<List<Note>>(json);
                sr.Close();

                if (notes != null)
                {
                    return notes;
                }
                else
                {
                    throw new NullReferenceException("There was nothing to read from the file and notes list is null.");
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                throw;
            }
        }


        public void WriteNotesToJson(List<Note> notes)
        {
            try
            {
                NotesFileCheck(JsonPath);
                StreamWriter sw = new StreamWriter(JsonPath);
                var json = JsonSerializer.Serialize(notes);
                sw.WriteLine(json);
                sw.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                throw;
            }
        }


         public void NotesFileCheck(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Notes file doesn't exist!");
            }
        }


    }
}


