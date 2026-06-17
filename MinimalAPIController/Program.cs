var builder = WebApplication.CreateBuilder(args);

#region MinimalAPI Swagger(Endpoint-leri gostermek ucun)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

builder.Services.AddControllers();

var app = builder.Build();

#region MinimalAPI Use Swagger(Middleware)
app.UseSwagger();
app.UseSwaggerUI();
#endregion


app.MapGet("get-minimal-api", () => "Hello World!");

app.MapControllers();

app.Run();
