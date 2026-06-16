var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("simple-string", () => "Hello World");
app.MapGet("json-raw-obj", () => new { Message = "Hello World" });
app.MapGet("ok-obj", () => Results.Ok(new { Message = "Hello World" }));
app.MapGet("json-obj", () => Results.Json(new { Message = "Hello World" }));
app.MapGet("text-string", () => Results.Text("Hello World"));
app.MapGet("redirect", () => Results.Redirect("https://google.com"));
app.MapGet("download", () => Results.File("C:\\Users\\e.q.asadov\\Desktop\\Bulk_Operations_Test.xlsx"));

app.MapGet("logging", (ILogger<Program> logger) =>
{
    logger.LogInformation("I added a log to this line");

    return Results.Ok();
});

app.Run();
