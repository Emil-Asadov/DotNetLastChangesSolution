var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("get-method-one", () => "Hello get method");
app.MapPost("post-method-one", () => "Hello post method");
app.MapGet("get-method-two", () => Results.Ok(new
{
    Message = "API is working..."
}));
app.MapGet("get-method-three", async () =>
{
    await Task.Delay(1000);
    return Results.Ok(new { Message = "API is working..." });
});


app.Run();
