using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyProxyAPI.Models
{
    public class DatabaseSettings
    {
        public String DatabaseName { get; set; }
        public String CollectionName { get; set; }
        public String ConnectionString { get; set; }
    }
}
