

using Newtonsoft.Json;

namespace SpotifyProxyAPI.Models
{
    public class AuthResponse
    {
        [JsonProperty(PropertyName ="access_token")]
        public string Access_token { get; set; }


        [JsonProperty(PropertyName = "token_type")]

        public string Token_type { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int Expires_in { get; set; }
    }
}
