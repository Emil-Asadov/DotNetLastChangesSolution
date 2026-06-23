namespace MinimalAPIRealProject.Models.Config
{
    public record DbConfigRecord
    {
        public string? Hostname { get; init; }
        public string? ServiceName { get; init; }
        public string? Username { get; init; }
        public string? Password { get; init; }
        public string? Port { get; init; }
    }
}
