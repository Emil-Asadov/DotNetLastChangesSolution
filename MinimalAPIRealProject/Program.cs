using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MinimalAPIRealProject.DB;
using MinimalAPIRealProject.FluentValidation;
using MinimalAPIRealProject.Models.Config;
using MinimalAPIRealProject.Models.Entity;
using MinimalAPIRealProject.Repository;
using MinimalAPIRealProject.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region DI
builder.Services.Configure<DbConfigRecord>(builder.Configuration.GetSection($"DbConfiguration")); //Prod baza- Prod; Test baza- Test
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection($"Jwt")); //Prod baza- Prod; Test baza- Test
builder.Services.AddScoped<DbConnect>();
builder.Services.AddScoped<DbOperation>();
builder.Services.AddScoped<IOperationRepository, OperationRepository>();
builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<JwtProvider>();
#endregion

#region Authentication and Authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization();
#endregion

var app = builder.Build();

#region Swagger
app.UseSwagger();
app.UseSwaggerUI();
#endregion

#region Minimal API Endpoints
app.MapGet("get-token", async ([FromBody] UserRequest apiUser, [FromServices] IOperationService bookService, [FromServices] JwtProvider jwtProvider, CancellationToken cancellationToken) =>
{
    var userRes = await bookService.CheckUserSrv(apiUser, cancellationToken);
    if (userRes.res is null)
        return Results.NotFound(new { Message = "User not found" });

    var token = jwtProvider.CreateToken(userRes.res);

    return Results.Ok(new { Token = token });
});

app.MapGet("get-all-books", [Authorize(Roles = "Admin,Guest")] async ([FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
{
    var res = await bookService.GetBooksListSrv(cancellationToken);
    var lst = res.lst.Values.ToList();

    return Results.Ok(lst);
});

app.MapGet("get-book-route/{isbn:length(6):regex(^[0-9-]+$)}", async ([FromRoute] string isbn, [FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
{
    var res = await bookService.GetBookSrv(isbn, cancellationToken);

    return Results.Ok(res.lst);
});

app.MapGet("get-book-query", async ([FromQuery(Name = "isbn")] string isbn, [FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
{
    var inputParam = new InputParam(Isbn: isbn);
    var validator = new InputParamValidator();
    var validationResult = validator.Validate(inputParam);
    if (!validationResult.IsValid)
        return Results.BadRequest(validationResult.Errors.Select(s => s.ErrorMessage));

    var res = await bookService.GetBookSrv(isbn, cancellationToken);

    return Results.Ok(res.lst);
});

app.MapPost("create-book", [Authorize(Roles = "Admin")] async ([FromBody] BookRequest bookRequest, [FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
{
    var validator = new BookValidator();
    var validationResult = validator.Validate(bookRequest);
    if (!validationResult.IsValid)
        return Results.BadRequest(validationResult.Errors.Select(s => s.ErrorMessage));

    var resPost = await bookService.PostBookSrv(bookRequest, cancellationToken);
    if (!string.IsNullOrWhiteSpace(resPost.err))
        return Results.BadRequest(resPost.err);

    return Results.Ok(new { ErrorCode = 0, Message = "Əməliyyat yerinə yetirildi" });
});

app.MapPost("update-book/{id:int}", async ([FromRoute] int id, [FromBody] BookRequest bookRequest, [FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
{
    var validator = new BookValidator();
    var validationResult = validator.Validate(bookRequest);
    if (!validationResult.IsValid)
        return Results.BadRequest(validationResult.Errors.Select(s => s.ErrorMessage));

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
#endregion

app.Run();
