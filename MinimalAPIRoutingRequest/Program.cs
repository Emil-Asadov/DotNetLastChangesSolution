using MinimalAPIRoutingRequest;
using System.Text;

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

app.MapGet("from-class-without-param", () => Example.SomeMethodWithoutParameter());
app.MapGet("from-class-without-param-new", Example.SomeMethodWithoutParameterNew);
app.MapGet("from-class-with-param", (string p) => Example.SomeMethodWithParameter(p));//Query params
app.MapGet("get-params/{age}", (int age) => new StringBuilder("Age provided was").Append((char)32).Append(age).ToString());//Route params
app.MapGet("get-params-new/{age:int}", (int age) => new StringBuilder("Age provided was new").Append((char)32).Append(age).ToString());//Route params

app.MapGet("cars/{carId:regex(^[a-z0-9]+$)}", (string carId) => new StringBuilder("Car provided was:").Append((char)32).Append(carId).ToString());//Route params
app.MapGet("books/{bookId:length(5)}", (string bookId) => new StringBuilder("Book provided was:").Append((char)32).Append(bookId).ToString());//Route params
app.MapGet("students/{studentId:length(5):regex(^[a-z0-9]+$)}", (string studentId) => new StringBuilder("Student provided was:").Append((char)32).Append(studentId).ToString());//Route params

app.Run();
