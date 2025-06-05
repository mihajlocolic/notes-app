using NotesAPI.Controllers;

namespace NotesAPI;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

    }
    [Fact]
    public void GetNotes_ShouldNotBeNull()
        {
            NoteController noteController = new NoteController();
            Assert.NotNull(noteController.GetAllNotes());
        }

        [Fact]
        public void GetNote_ValueOf1_ShouldNotBeNull()
        {
            NoteController noteController = new NoteController();
            Assert.NotNull(noteController.GetNote(1));
        }

    [Fact]
    public void GetNote_ValueOf10_ShouldBeNull()
    {
        NoteController noteController = new NoteController();
        Assert.NotNull(noteController.GetNote(10));
    }
}
