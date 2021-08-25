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
        /// <summary>
        /// query requested
        /// </summary>
        public string Query { get; set; }
       
        /// <summary>
        /// List of top 5 artists
        /// </summary>
        public IEnumerable< Top5Artists> Value { get; set; }
    }
    public class Top5Artists
    {
        /// <summary>
        /// Id of the artist given by Spotify
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the artist
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Number of followers for the artist
        /// </summary>
        public int Followers { get; set; }

        /// <summary>
        /// Popularity of the artist
        /// </summary>
        public int Popularity { get; set; }

        /// <summary>
        /// Web request link for the artist profile
        /// </summary>
        public string Link { get; set; }
    }
}
