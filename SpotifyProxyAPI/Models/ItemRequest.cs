
using System.ComponentModel.DataAnnotations;

namespace SpotifyProxyAPI.Models
{
    /// <summary>
    /// SpotifyProxyAPI request model classs
    /// </summary>
    public class ItemRequest
    {

        /// <summary>
        /// Query field in the request which is mandatory
        /// </summary>
        [Required]
        public string Query { get; set; }
       
    }
}
