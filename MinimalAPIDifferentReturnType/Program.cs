var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("get-method-one", () => Results.Ok(new
{
    Message = "API is working..."
}));
app.MapGet("get-method-two", async () =>
{
    await Task.Delay(1000);
    return Results.Ok(new { Message = "API is working..." });
});

app.Run();
