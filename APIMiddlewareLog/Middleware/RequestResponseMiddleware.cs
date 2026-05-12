using APIMiddlewareLog.Helper;
using APIMiddlewareLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using System.Text;

namespace APIMiddlewareLog.Middleware
{
    public class RequestResponseMiddleware(ILogger<RequestResponseMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            #region Comment Code
            //var orgBodyStream = context.Response.Body;
            //_logger.LogInformation("Query Keys: {QueryKeys}", context.Request.QueryString);

            //var requestBody = new MemoryStream();
            //await context.Request.Body.CopyToAsync(requestBody);
            //requestBody.Seek(0, SeekOrigin.Begin);
            //var requestText = await new StreamReader(requestBody).ReadToEndAsync();
            //requestBody.Seek(0, SeekOrigin.Begin);

            //var tempStream = new MemoryStream();
            //context.Response.Body = tempStream;

            //await next.Invoke(context);

            //context.Response.Body.Seek(0, SeekOrigin.Begin);
            //var responseText = await new StreamReader(context.Response.Body, Encoding.UTF8).ReadToEndAsync();
            //context.Response.Body.Seek(0, SeekOrigin.Begin);

            //await context.Response.Body.CopyToAsync(orgBodyStream);

            //_logger.LogInformation("Request: {requestText}", requestText);
            //_logger.LogInformation("Response: {responseText}", responseText);
            #endregion

            var logGuid = Guid.NewGuid().ToString();
            context.Items["LogGuid"] = logGuid;
            var request = await LogRequest(context, logGuid);
            var response = string.Empty;

            var originalResponseBody = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                try
                {
                    await next(context);
                    response = await LogResponse(context, responseBody, originalResponseBody, logGuid);
                }
                catch (Exception ex)
                {
                    logger.LogError($"An exception occurred: {ex.Message}");
                }
                finally
                {
                    context.Response.Body = originalResponseBody;
                }
            }

            logger.LogInformation("Request: {request}", request);
            logger.LogInformation("Response: {response}", response);

            var settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            try
            {
                request = JsonConvert.DeserializeObject(request, settings)?.ToString();
            }
            catch (Exception ex)
            {
                logger.LogError($"JsonConvert.DeserializeObject returned error(Response): {ex.Message}");
            }
            try
            {
                response = JsonConvert.DeserializeObject(response, settings)?.ToString();
            }
            catch (Exception ex)
            {
                logger.LogError($"JsonConvert.DeserializeObject returned error(Request): {ex.Message}");
            }

            var clsLogEntity = new LogEntity
             (
                LogGuid: logGuid,
                HttpMethod: context.Request.Method,
                Url: context.Request.Path,
                Request: request!,
                Response: response!
             );
            var resIgnoreField = JsonIgnoreField.GetJson(clsLogEntity);
            logger.LogInformation(resIgnoreField);
        }
        private async Task<string> LogRequest(HttpContext context, string logGuid)
        {
            context.Request.EnableBuffering();

            var content = string.Empty;
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                content = await reader.ReadToEndAsync();
            }

            context.Request.Body.Position = 0;
            var clientIp = GetClientIpFromHeaders(context); //context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var requestLog = new LogEntry
            (
                LogGuid: logGuid,
                HttpMethod: context.Request.Method,
                Url: context.Request.Path,
                Headers: string.Join("; ", context.Request.Headers.Select(h => $"{h.Key}: {h.Value}")),
                Body: content,
                Type: "Request",
                StatusCode: null,
                IpAdress: clientIp,
                InsertDate: DateTime.Now
            );

            //await _saveLogs.SaveLogAsync(requestLog); //Write to DB

            logger.LogInformation(content);

            return content;
        }

        private async Task<string> LogResponse(HttpContext context, MemoryStream responseBody, Stream originalResponseBody, string logGuid)
        {
            responseBody.Seek(0, SeekOrigin.Begin);

            var content = string.Empty;
            using (var reader = new StreamReader(responseBody, Encoding.UTF8, leaveOpen: true))
            {
                content = await reader.ReadToEndAsync();
            }

            responseBody.Seek(0, SeekOrigin.Begin);
            await using (var writer = new MemoryStream())
            {
                await responseBody.CopyToAsync(writer);
                writer.Seek(0, SeekOrigin.Begin);
                await writer.CopyToAsync(originalResponseBody);
            }

            context.Response.Body = originalResponseBody;

            var responseLog = new LogEntry
            (
                LogGuid: logGuid,
                HttpMethod: context.Request.Method,
                Url: context.Request.Path,
                Headers: string.Join("; ", context.Response.Headers.Select(h => $"{h.Key}: {h.Value}")),
                Body: content,
                Type: "Response",
                StatusCode: context.Response.StatusCode,
                InsertDate: DateTime.Now,
                IpAdress: null
            );

            //await _saveLogs.SaveLogAsync(responseLog); //Write to DB

            logger.LogInformation(content);

            return content;
        }

        private string GetClientIpFromHeaders(HttpContext context)
        {
            var headersToCheck = new[] { "X-Forwarded-For", "X-Real-IP" };

            foreach (var header in headersToCheck)
            {
                if (context.Request.Headers.TryGetValue(header, out var headerValue))
                {
                    var clientIp = headerValue.ToString().Split(',').FirstOrDefault();
                    if (!string.IsNullOrEmpty(clientIp))
                    {
                        return clientIp.Trim();
                    }
                }
            }

            return context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        }
    }
}
