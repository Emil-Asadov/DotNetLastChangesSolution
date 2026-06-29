namespace MinimalAPIRealProject.Service
{
    public record TokenResponse(string AccessToken, string TokenType = "Bearer");
}
