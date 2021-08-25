using SpotifyProxyAPI.Helpers;

namespace SpotifyProxyAPI.Models
{
    /// <summary>
    /// User Settings parameters
    /// </summary>
    public class UserSettings
    {
        /// <summary>
        /// MiniProfilers basepath 
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// Enabling miniProfiler(boolean)
        /// </summary>
        public string EnableMiniProfiler { get; set; }

        /// <summary>
        /// Spotify settings 
        /// </summary>
        public SpotifySettings SpotifySettings { get; set; }

        /// <summary>
        /// Auditlog settings
        /// </summary>
        public AuditLog AuditLog { get; set; }
    }

    /// <summary>
    /// Spotify settings
    /// </summary>
    public class SpotifySettings
    {
        /// <summary>
        /// Client Id provided by Spotify
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Client Secret provided by Spotify
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// The Uri for token 
        /// </summary>
        public string TokenUri { get; set; }

        /// <summary>
        /// Item search endpoint base path
        /// </summary>
        public string ItemSearchBaseUri { get; set; }
    }
}
