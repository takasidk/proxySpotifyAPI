

using Newtonsoft.Json;

namespace SpotifyProxyAPI.Models
{
    /// <summary>
    /// Custom Error Response class
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Message describing the error
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Http Status code of the response
        /// </summary>
        public int StatusCode { get; set; }
    }

    
}
