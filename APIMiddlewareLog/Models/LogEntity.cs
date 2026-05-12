namespace APIMiddlewareLog.Models
{
    public record LogEntity(string LogGuid, string HttpMethod, string Url,string Request,string Response);
}
