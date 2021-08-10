using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyProxyAPI.Models
{
    public class ItemRequest
    {
        [Required]
        public string Query { get; set; }
        /*[Required]
        public string type { get; set; }*/
        [Required]
        public string market { get; set; }
    }
}
