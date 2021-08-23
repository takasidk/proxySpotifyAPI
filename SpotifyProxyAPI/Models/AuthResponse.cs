

using Newtonsoft.Json;

namespace SpotifyProxyAPI.Models
{
    /// <summary>
    /// Oauth response model class
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// Spotify access token
        /// </summary>
        [JsonProperty(PropertyName ="access_token")]
        public string Access_token { get; set; }

        /// <summary>
        /// Type of token returned
        /// </summary>
        [JsonProperty(PropertyName = "token_type")]
        public string Token_type { get; set; }

        /// <summary>
        /// Expire time in seconds
        /// </summary>
        [JsonProperty(PropertyName = "expires_in")]
        public int Expires_in { get; set; }
    }
}
