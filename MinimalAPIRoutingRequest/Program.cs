using MinimalAPIRoutingRequest;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("get", () => "This is a GET method");
app.MapPost("post", () => "This is a POST method");
app.MapPut("put", () => "This is a PUT method");
app.MapDelete("delete", () => "This is a DELETE method");
app.MapMethods("head", new[] { "HEAD" }, () => Results.Ok());//HEAD /head ? returns only headers, no body.
app.MapMethods("options", new[] { "OPTIONS" }, () => "This is a OPTIONS method");
app.MapMethods("options-or-head", new[] { "HEAD", "OPTIONS" }, () => "These are HEAD and OPTIONS methods");

var handler = () => "This is coming from var";
app.MapGet("handler", handler);

app.MapGet("from-class-with-param", () => Example.SomeMethodWithoutParameter());
app.MapGet("from-class-without-param", (string p) => Example.SomeMethodWithParameter(p));

app.Run();
