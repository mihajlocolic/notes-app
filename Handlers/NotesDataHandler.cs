
using NotesAPI.Models;
using System.Text.Json;

namespace NotesAPI.Handlers
{
    public class NotesDataHandler
    {
        private string JsonPath = "notes.json";

        public List<Note> ReadNotesFromJson()
        {
            List<Note>? n = new List<Note>();

            try
            {
                StreamReader sr = new StreamReader(JsonPath);
                string json = sr.ReadToEnd();
                n = JsonSerializer.Deserialize<List<Note>>(json);
                sr.Close();
            }
            catch (FileNotFoundException FileNotFoundException)
            {
                Console.WriteLine(FileNotFoundException.Message);
            }


            if (n != null)
            {
                return n;
            }
            else
            {
                throw new NullReferenceException("ReadNotesFromJson(): Returned list of notes was null.");
            }

        }


        public void WriteNotesToJson(List<Note> notes)
        {
            try
            {
                StreamWriter sw = new StreamWriter(JsonPath);
                var json = JsonSerializer.Serialize(notes);
                sw.WriteLine(json);
                sw.Close();
            }
            catch (FileNotFoundException FileNotFoundException)
            {
                Console.WriteLine(FileNotFoundException.Message);
            }
        }

    
    }
}


