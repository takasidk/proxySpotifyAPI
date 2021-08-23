using SpotifyProxyAPI.Helpers;

namespace SpotifyProxyAPI.Models
{
    /// <summary>
    /// User Settings parameters
    /// </summary>
    public class UserSettings
    {
        public string BasePath { get; set; }
        public string EnableMiniProfiler { get; set; }
        public SpotifySettings SpotifySettings { get; set; }
        public AuditLog AuditLog { get; set; }
    }

    /// <summary>
    /// Spotify settings
    /// </summary>
    public class SpotifySettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public string TokenUri { get; set; }

        public string ItemSearchBaseUri { get; set; }
    }
}
