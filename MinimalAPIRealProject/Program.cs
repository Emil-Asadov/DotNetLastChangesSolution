using Microsoft.AspNetCore.Mvc;
using MinimalAPIRealProject.DB;
using MinimalAPIRealProject.Models.Config;
using MinimalAPIRealProject.Models.Entity;
using MinimalAPIRealProject.Repository;
using MinimalAPIRealProject.Service;

var builder = WebApplication.CreateBuilder(args);

#region Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region DI
builder.Services.Configure<DbConfigRecord>(builder.Configuration.GetSection($"DbConfiguration")); //Prod baza- Prod; Test baza- Test
builder.Services.AddScoped<DbConnect>();
builder.Services.AddScoped<DbOperation>();
builder.Services.AddScoped<IOperationRepository, OperationRepository>();
builder.Services.AddScoped<IOperationService, OperationService>();
#endregion

var app = builder.Build();

#region Swagger
app.UseSwagger();
app.UseSwaggerUI();
#endregion

app.MapGet("get-all-books", async ([FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
{
    var res = await bookService.GetBooksListSrv(cancellationToken);
    var lst = res.lst.Values.ToList();

    return Results.Ok(lst);
});

app.MapGet("get-book/{isbn}", async ([FromRoute] string isbn, [FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
{
    var res = await bookService.GetBookSrv(isbn, cancellationToken);

    return Results.Ok(res.lst);
});

app.MapPost("create-book", async ([FromBody] BookRequest bookRequest, [FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
{
    var resPost = await bookService.PostBookSrv(bookRequest, cancellationToken);
    if (!string.IsNullOrWhiteSpace(resPost.err))
        return Results.BadRequest(resPost.err);

    return Results.Ok(new { ErrorCode = 0, Message = "Əməliyyat yerinə yetirildi" });
});

app.MapPost("update-book/{id:int}", async ([FromRoute] int id, [FromBody] BookRequest bookRequest, [FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
{
    var resPost = await bookService.UpdateBookSrv(id, bookRequest, cancellationToken);
    if (!string.IsNullOrWhiteSpace(resPost.err))
        return Results.BadRequest(resPost.err);

    return Results.Ok(new { ErrorCode = 0, Message = "Əməliyyat yerinə yetirildi" });
});

app.MapPost("delete-book/{id:int}", async ([FromRoute] int id, [FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
{
    var resPost = await bookService.DeleteBookSrv(id, cancellationToken);
    if (!string.IsNullOrWhiteSpace(resPost.err))
        return Results.BadRequest(resPost.err);

    return Results.Ok(new { ErrorCode = 0, Message = "Əməliyyat yerinə yetirildi" });
});

app.Run();
