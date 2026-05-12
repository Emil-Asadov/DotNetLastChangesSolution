namespace APIMiddlewareLog.Models
{
    public record LogEntry(string LogGuid, string HttpMethod, string Url, string Headers, string Body, string Type, int? StatusCode, DateTime InsertDate, string? IpAdress);
}
