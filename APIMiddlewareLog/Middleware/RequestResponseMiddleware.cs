using Microsoft.AspNetCore.Http;
using System.Text;

namespace APIMiddlewareLog.Middleware
{
    public class RequestResponseMiddleware : IMiddleware
    {
        private readonly ILogger<RequestResponseMiddleware> _logger;

        public RequestResponseMiddleware(ILogger<RequestResponseMiddleware> logger)
        {
            _logger = logger;
        }
        //public async Task Invoke(HttpContext context)
        //{
        //    //Request
        //    var orgBodyStream = context.Response.Body;
        //    logger.LogInformation("Query Keys: {QueryKeys}", context.Request.QueryString);

        //    var requestBody = new MemoryStream();
        //    await context.Request.Body.CopyToAsync(requestBody);
        //    requestBody.Seek(0, SeekOrigin.Begin);
        //    var requestText = await new StreamReader(requestBody).ReadToEndAsync();
        //    requestBody.Seek(0, SeekOrigin.Begin);

        //    var tempStream = new MemoryStream();
        //    context.Response.Body = tempStream;

        //    await requestDelegate.Invoke(context);

        //    context.Response.Body.Seek(0, SeekOrigin.Begin);
        //    var responseText = await new StreamReader(context.Response.Body, Encoding.UTF8).ReadToEndAsync();
        //    context.Response.Body.Seek(0, SeekOrigin.Begin);

        //    await context.Response.Body.CopyToAsync(orgBodyStream);

        //    logger.LogInformation("Request: {requestText}", requestText);
        //    logger.LogInformation("Response: {responseText}", responseText);
        //}

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var orgBodyStream = context.Response.Body;
            _logger.LogInformation("Query Keys: {QueryKeys}", context.Request.QueryString);

            var requestBody = new MemoryStream();
            await context.Request.Body.CopyToAsync(requestBody);
            requestBody.Seek(0, SeekOrigin.Begin);
            var requestText = await new StreamReader(requestBody).ReadToEndAsync();
            requestBody.Seek(0, SeekOrigin.Begin);

            var tempStream = new MemoryStream();
            context.Response.Body = tempStream;

            await next.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(context.Response.Body, Encoding.UTF8).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            await context.Response.Body.CopyToAsync(orgBodyStream);

            _logger.LogInformation("Request: {requestText}", requestText);
            _logger.LogInformation("Response: {responseText}", responseText);
        }
    }
}
