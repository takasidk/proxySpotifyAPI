using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyProxyAPI.Models
{
    [BsonIgnoreExtraElements]

    public class ResponseDTO
    {
        public string Query { get; set; }
        public string Market { get; set; }
        public IEnumerable< Top5Artists> ArtistsList { get; set; }
    }
    public class Top5Artists
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Followers { get; set; }
        public int Popularity { get; set; }
        public string Link { get; set; }
    }
}
