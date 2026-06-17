var builder = WebApplication.CreateBuilder(args);

#region MinimalAPI Swagger(Endpoint-leri gostermek ucun)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

var app = builder.Build();

#region MinimalAPI Use Swagger(Middleware)
app.UseSwagger();
app.UseSwaggerUI();
#endregion

app.MapGet("/", () => "Hello World!");
app.MapGet("simple-string", () => "Hello World");
app.MapGet("json-raw-obj", () => new { Message = "Hello World" });
app.MapGet("ok-obj", () => Results.Ok(new { Message = "Hello World" }));
app.MapGet("json-obj", () => Results.Json(new { Message = "Hello World" }));
app.MapGet("text-string", () => Results.Text("Hello World"));

app.Run();
