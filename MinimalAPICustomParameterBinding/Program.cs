using Microsoft.AspNetCore.Mvc;
using MinimalAPICustomParameterBinding.Entity;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("get-point-single", (MapPoint mapPoint) =>
{
    return Results.Ok(mapPoint);
});

app.MapGet("get-point-multiple", (MapPoint mapPoint, Employee employee) =>
{
    return Results.Ok(employee);
});

app.Run();
