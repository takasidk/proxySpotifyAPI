

namespace SpotifyProxyAPI.Helpers
{
    public class AuditLog
    {
        public string Path { get; set; } 
        public string RollingInterval { get; set; } 
        public bool Shared { get; set; } 
        public int RetainedFileCountLimit { get; set; } 

    }
}
