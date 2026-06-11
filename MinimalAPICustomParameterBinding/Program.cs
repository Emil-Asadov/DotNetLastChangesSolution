using MinimalAPICustomParameterBinding.Entity;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("get-point", (MapPoint mapPoint) =>
{
    return Results.Ok(mapPoint);
});

app.Run();
