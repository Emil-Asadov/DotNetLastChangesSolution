namespace APIMiddlewareLog.Models
{
    public record LogRequest(string request, string response, string operationType, int responseStatusCode, int operationId = 0, int dataRowId = 0, string? uniqueNumber = null);
}
