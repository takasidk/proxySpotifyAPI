

namespace SpotifyProxyAPI.Helpers
{
    public class AuditLog
    {
        public string Path { get; set; } //= "logs\\audit\\log-.log";
        public string RollingInterval { get; set; } //= "Day";
        public bool Shared { get; set; } //= true;
        public int RetainedFileCountLimit { get; set; } //= 10;

    }
}
