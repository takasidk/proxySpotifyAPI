
using System.ComponentModel.DataAnnotations;

namespace SpotifyProxyAPI.Models
{
    public class ItemRequest
    {
        [Required]
        public string Query { get; set; }
       
    }
}
