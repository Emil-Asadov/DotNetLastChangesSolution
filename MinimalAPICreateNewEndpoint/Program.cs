var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("get-method-one", () => "Hello get method");
app.MapPost("post-method-one", () => "Hello post method");

app.Run();
