using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("httpContext", async (HttpContext context) =>
{
    await context.Response.WriteAsync("Hello from the httpContext");
});

app.MapGet("http/{id}", async (HttpRequest request, HttpResponse response, [FromBody] Person person) =>
{
    var queryParams = request.QueryString;
    var routeParams = string.Empty;

    foreach (var item in request.RouteValues)
    {
        if (string.IsNullOrWhiteSpace(routeParams))
            routeParams = $"{item.Key}-{item.Value}";
        else
            routeParams = $"{routeParams};{(char)32}{item.Key}-{item.Value}";
    }
    await response.WriteAsync($"Hello from the http:{(char)13}{(char)10}Route value is: {routeParams}{(char)13}{(char)10}Query params is: {queryParams}");
});

app.MapGet("http1/{id}&{name}", async (HttpRequest request, HttpResponse response) =>
{
    var queryParams = request.QueryString;
    var routeParams = string.Empty;

    foreach (var item in request.RouteValues)
    {
        if (string.IsNullOrWhiteSpace(routeParams))
            routeParams = $"{item.Key}-{item.Value}";
        else
            routeParams = $"{routeParams};{(char)32}{item.Key}-{item.Value}";
    }
    await response.WriteAsync($"Hello from the http:{(char)13}{(char)10}Route value is: {routeParams}{(char)13}{(char)10}Query params is: {queryParams}");
});

app.MapGet("claims", (ClaimsPrincipal user) =>
{
    var claims = user.Claims.ToList();

    return Results.Ok(claims);
});

app.MapGet("cancel", (CancellationToken cancellationToken) =>
{
    return Results.Ok();
});

app.Run();
