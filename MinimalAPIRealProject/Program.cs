using Microsoft.AspNetCore.Mvc;
using MinimalAPIRealProject.DB;
using MinimalAPIRealProject.Models.Config;
using MinimalAPIRealProject.Models.Entity;
using MinimalAPIRealProject.Repository;
using MinimalAPIRealProject.Service;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<DbConfigRecord>(builder.Configuration.GetSection($"DbConfiguration")); //Prod baza- Prod; Test baza- Test

builder.Services.AddSingleton<DbConnect>();
builder.Services.AddSingleton<DbOperation>();
builder.Services.AddSingleton<OperationRepository>();
builder.Services.AddSingleton<OperationService>();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("get-books", async ([FromServices] OperationService bookService, CancellationToken cancellationToken) =>
{
    var res = await bookService.GetBooksListSrv(cancellationToken);
    var lst = res.lst.Values.ToList();

    return Results.Ok(lst);
});

app.MapGet("get-book/{isbn}", async ([FromRoute] string isbn, [FromServices] OperationService bookService, CancellationToken cancellationToken) =>
{
    var res = await bookService.GetBookSrv(isbn, cancellationToken);

    return Results.Ok(res.lst);
});

app.MapPost("create-book", async ([FromBody] BookRequest bookRequest, [FromServices] OperationService bookService, CancellationToken cancellationToken) =>
{
    var resPost = await bookService.PostBookSrv(bookRequest, cancellationToken);
    if (!string.IsNullOrWhiteSpace(resPost.err))
        return Results.BadRequest(resPost.err);

    return Results.Ok(new { ErrorCode = 0, Message = "Əməliyyat yerinə yetirildi" });
});

app.Run();
