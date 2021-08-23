using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;


namespace SpotifyProxyAPI.Models
{
    /// <summary>
    /// Final Response class which has querry and array of top 5 artist objects
    /// </summary>
    [BsonIgnoreExtraElements]

    
    public class ResponseDto
    {
        public string Query { get; set; }
       
        public IEnumerable< Top5Artists> Value { get; set; }
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
