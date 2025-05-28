var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.MapGet("/", () => "NotesAPI\n\nHead over to /notes for list of all the notes!");


// Necessary for a manual controller route to work.
app.MapControllerRoute(
    name: "notes",
    pattern: "{controller=NoteController}/{action=GetAllNotes}");

app.Run();
