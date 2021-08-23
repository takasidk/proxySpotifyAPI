
using Newtonsoft.Json;

namespace SpotifyProxyAPI.Models
{
    /// <summary>
    /// Response model class which contains Array of Item objects wrapped inside a Artists Object
    /// </summary>
    public class ItemResponse
    {
        [JsonProperty(PropertyName ="artists")]
        public Artists Artists { get; set; }
    }
    public class Artists
    {
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        [JsonProperty(PropertyName = "items")]
        public Items[] Items { get; set; }

        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }

        [JsonProperty(PropertyName = "next")]
        public string Next { get; set; }

        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

        [JsonProperty(PropertyName = "previous")]
        public string Previous { get; set; }

        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }
    }

    public class Items 
    {
        [JsonProperty(PropertyName = "external_urls")]
        public ExternalUrls External_urls { get; set; }

        [JsonProperty(PropertyName = "followers")]
        public Followers Followers { get; set; }

        [JsonProperty(PropertyName = "genres")]
        public string[] Genres { get; set; }

        [JsonProperty(PropertyName = "Href")]
        public string Href { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "images")]
        public Image[] Images { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "popularity")]
        public int Popularity { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }

    
    }
    public class ExternalUrls
    {
        [JsonProperty(PropertyName = "spotify")]
        public string Spotify { get; set; }
    }

    public class Followers
    {
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }
    }
    public class Image
    {
        [JsonProperty(PropertyName = "height")]
        public int Height { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }
    }

}
