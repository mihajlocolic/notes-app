var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.Logger.LogInformation("The notes app started.");

app.MapGet("/", () => "NotesAPI\n\nHead over to /notes for list of all the notes!");
app.MapControllers(); // All you have to do if you're creating controllers and routes manually.

app.Run();
