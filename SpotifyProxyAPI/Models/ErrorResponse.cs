

using Newtonsoft.Json;

namespace SpotifyProxyAPI.Models
{
    public class ErrorResponse
    {
        [JsonProperty(PropertyName ="error")]
        public Error Error { get; set; }
    }

    public class Error
    {
        [JsonProperty(PropertyName = "message")]
        public string ErrorMessage { get; set; }

        [JsonProperty(PropertyName = "status")]
        public int StatusCode { get; set; }
    }
}
