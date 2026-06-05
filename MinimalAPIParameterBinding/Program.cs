using Microsoft.AspNetCore.Mvc;
using MinimalAPIParameterBinding.Entity;
using MinimalAPIParameterBinding.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<PeopleService>();//DI
builder.Services.AddScoped<GuidGenerator>();//DI

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("people/search", (string? searchTerm, PeopleService peopleService) =>
{
    if (string.IsNullOrWhiteSpace(searchTerm))
        return Results.NotFound();

    var result = peopleService.Serach(searchTerm);
    if (!result.Any())
        return Results.NoContent();

    return Results.Ok(result);
});

app.MapGet("get-mix/{routeParams}", ([FromRoute] string routeParams, [FromQuery(Name = "par")] int queryParams, [FromServices] GuidGenerator guidGenerator) =>
{
    var res = new StringBuilder(routeParams).Append((char)32).Append(queryParams).Append((char)32).Append(guidGenerator.NewGuid).ToString();

    return res;
});

app.MapPost("people", ([FromBody] Person person, [FromServices] PeopleService peopleService) =>
{
    var res = peopleService.Add(person);


    return Results.Ok(res);
});

app.Run();
