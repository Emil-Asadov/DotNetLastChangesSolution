using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIRealProject.FluentValidation;
using MinimalAPIRealProject.Models.Entity;
using MinimalAPIRealProject.Service;

namespace MinimalAPIRealProject.Endpoints
{
    public static class BookEndpoints
    {
        public static void UseBookEndpoints(this IEndpointRouteBuilder app)
        {
            #region Minimal API Endpoints
            app.MapGet("/api/Book/get-token", async ([FromBody] UserRequest apiUser, [FromServices] IOperationService bookService, [FromServices] JwtProvider jwtProvider, CancellationToken cancellationToken) =>
            {
                var userRes = await bookService.CheckUserSrv(apiUser, cancellationToken);
                if (userRes.res is null)
                    return Results.NotFound(new { Message = "User not found" });

                var token = jwtProvider.CreateToken(userRes.res);

                return Results.Ok(new { Token = token });
            }).WithTags("Auth");

            app.MapGet("/api/Book/get-all-books", [Authorize(Roles = "Admin,Guest")] async ([FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
            {
                var res = await bookService.GetBooksListSrv(cancellationToken);
                var lst = res.lst.Values.ToList();

                return Results.Ok(lst);
            }).WithTags("Books");

            app.MapGet("/api/Book/get-book-route/{isbn:length(6):regex(^[0-9-]+$)}", async ([FromRoute] string isbn, [FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
            {
                var res = await bookService.GetBookSrv(isbn, cancellationToken);

                return Results.Ok(res.lst);
            }).WithName("get-book-isbn").WithTags("Books");

            app.MapGet("/api/Book/get-book-query", async ([FromQuery(Name = "isbn")] string isbn, [FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
            {
                var inputParam = new InputParam(Isbn: isbn);
                var validator = new InputParamValidator();
                var validationResult = validator.Validate(inputParam);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors.Select(s => s.ErrorMessage));

                var res = await bookService.GetBookSrv(isbn, cancellationToken);

                return Results.Ok(res.lst);
            }).WithTags("Books");

            app.MapPost("/api/Book/create-book", [Authorize(Roles = "Admin")] async ([FromBody] BookRequest bookRequest, [FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
            {
                var validator = new BookValidator();
                var validationResult = validator.Validate(bookRequest);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors.Select(s => s.ErrorMessage));

                var resPost = await bookService.PostBookSrv(bookRequest, cancellationToken);
                if (!string.IsNullOrWhiteSpace(resPost.err))
                    return Results.BadRequest(resPost.err);

                //return Results.Ok(new { ErrorCode = 0, Message = "Əməliyyat yerinə yetirildi" });
                //return Results.Created($"/get-book-route/{bookRequest.Isbn}", bookRequest);
                return Results.CreatedAtRoute("get-book-isbn", new { isbn = bookRequest.Isbn });
            }).WithTags("Books");

            app.MapPost("/api/Book/update-book/{id:int}", async ([FromRoute] int id, [FromBody] BookRequest bookRequest, [FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
            {
                var validator = new BookValidator();
                var validationResult = validator.Validate(bookRequest);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors.Select(s => s.ErrorMessage));

                var resPost = await bookService.UpdateBookSrv(id, bookRequest, cancellationToken);
                if (!string.IsNullOrWhiteSpace(resPost.err))
                    return Results.BadRequest(resPost.err);

                return Results.Ok(new { ErrorCode = 0, Message = "Əməliyyat yerinə yetirildi" });
            }).WithTags("Books");

            app.MapPost("/api/Book/delete-book/{id:int}", async ([FromRoute] int id, [FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
            {
                var resPost = await bookService.DeleteBookSrv(id, cancellationToken);
                if (!string.IsNullOrWhiteSpace(resPost.err))
                    return Results.BadRequest(resPost.err);

                return Results.Ok(new { ErrorCode = 0, Message = "Əməliyyat yerinə yetirildi" });
            }).WithTags("Books");
            #endregion
        }

        public static RouteGroupBuilder MapBookEndpoints(this RouteGroupBuilder app)
        {
            #region Minimal API Endpoints
            app.MapGet("get-token", async ([FromBody] UserRequest apiUser, [FromServices] IOperationService bookService, [FromServices] JwtProvider jwtProvider, CancellationToken cancellationToken) =>
            {
                var userRes = await bookService.CheckUserSrv(apiUser, cancellationToken);
                if (userRes.res is null)
                    return Results.NotFound(new { Message = "User not found" });

                var token = jwtProvider.CreateToken(userRes.res);

                return Results.Ok(new { Token = token });
            }).WithTags("Auth");

            app.MapGet("get-all-books", [Authorize(Roles = "Admin,Guest")] async ([FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
            {
                var res = await bookService.GetBooksListSrv(cancellationToken);
                var lst = res.lst.Values.ToList();

                return Results.Ok(lst);
            }).WithTags("Books");

            app.MapGet("get-book-route/{isbn:length(6):regex(^[0-9-]+$)}", async ([FromRoute] string isbn, [FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
            {
                var res = await bookService.GetBookSrv(isbn, cancellationToken);

                return Results.Ok(res.lst);
            }).WithName("get-book-isbn").WithTags("Books");

            app.MapGet("get-book-query", async ([FromQuery(Name = "isbn")] string isbn, [FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
            {
                var inputParam = new InputParam(Isbn: isbn);
                var validator = new InputParamValidator();
                var validationResult = validator.Validate(inputParam);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors.Select(s => s.ErrorMessage));

                var res = await bookService.GetBookSrv(isbn, cancellationToken);

                return Results.Ok(res.lst);
            }).WithTags("Books");

            app.MapPost("create-book", [Authorize(Roles = "Admin")] async ([FromBody] BookRequest bookRequest, [FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
            {
                var validator = new BookValidator();
                var validationResult = validator.Validate(bookRequest);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors.Select(s => s.ErrorMessage));

                var resPost = await bookService.PostBookSrv(bookRequest, cancellationToken);
                if (!string.IsNullOrWhiteSpace(resPost.err))
                    return Results.BadRequest(resPost.err);

                //return Results.Ok(new { ErrorCode = 0, Message = "Əməliyyat yerinə yetirildi" });
                //return Results.Created($"/get-book-route/{bookRequest.Isbn}", bookRequest);
                return Results.CreatedAtRoute("get-book-isbn", new { isbn = bookRequest.Isbn });
            }).WithTags("Books");

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
            }).WithTags("Books");

            app.MapPost("delete-book/{id:int}", async ([FromRoute] int id, [FromServices] IOperationService bookService, CancellationToken cancellationToken) =>
            {
                var resPost = await bookService.DeleteBookSrv(id, cancellationToken);
                if (!string.IsNullOrWhiteSpace(resPost.err))
                    return Results.BadRequest(resPost.err);

                return Results.Ok(new { ErrorCode = 0, Message = "Əməliyyat yerinə yetirildi" });
            }).WithTags("Books");
            #endregion

            return app;
        }
    }
}
