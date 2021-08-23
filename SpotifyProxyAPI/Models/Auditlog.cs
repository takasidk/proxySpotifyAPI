

namespace SpotifyProxyAPI.Helpers
{
    /// <summary>
    /// Auditlog Settings parameters
    /// </summary>
    public class AuditLog
    {
        /// <summary>
        /// Path where audit logs are stored
        /// </summary>
        public string Path { get; set; } 

        /// <summary>
        /// Time to roll the logs into log file
        /// </summary>
        public string RollingInterval { get; set; } 

        /// <summary>
        /// Shared mode
        /// </summary>
        public bool Shared { get; set; } 

        /// <summary>
        /// File count limit
        /// </summary>
        public int RetainedFileCountLimit { get; set; } 

    }
}
