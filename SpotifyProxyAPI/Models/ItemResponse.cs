
using Newtonsoft.Json;

namespace SpotifyProxyAPI.Models
{
    /// <summary>
    /// Response model class which contains Array of Item objects wrapped inside a Artists Object
    /// </summary>
    public class ItemResponse
    {
        /// <summary>
        /// Paging class
        /// </summary>
        [JsonProperty(PropertyName ="artists")]
        public Artists Artists { get; set; }
    }
    public class Artists
    {
        /// <summary>
        /// A link to the Web API endpoint returning the full result of the request
        /// </summary>
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        /// <summary>
        /// The requested content
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public Items[] Items { get; set; }

        /// <summary>
        /// The maximum number of items in the response (as set in the query or by default).
        /// </summary>
        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }

        /// <summary>
        /// URL to the next page of items. ( null if none)
        /// </summary>
        [JsonProperty(PropertyName = "next")]
        public string Next { get; set; }

        /// <summary>
        /// The offset of the items returned (as set in the query or by default)	
        /// </summary>
        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

        /// <summary>
        /// URL to the previous page of items. ( null if none)
        /// </summary>
        [JsonProperty(PropertyName = "previous")]
        public string Previous { get; set; }

        /// <summary>
        /// The total number of items available to return.
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }
    }

    public class Items 
    {
        /// <summary>
        /// Known external URLs for this artist.
        /// </summary>
        [JsonProperty(PropertyName = "external_urls")]
        public ExternalUrls External_urls { get; set; }

        /// <summary>
        /// Information about the followers of the artist
        /// </summary>
        [JsonProperty(PropertyName = "followers")]
        public Followers Followers { get; set; }

        /// <summary>
        /// A list of the genres the artist is associated with. If not yet classified, the array is empty.
        /// </summary>
        [JsonProperty(PropertyName = "genres")]
        public string[] Genres { get; set; }


        /// <summary>
        /// A link to the Web API endpoint providing full details of the artist.
        /// </summary>
        [JsonProperty(PropertyName = "Href")]
        public string Href { get; set; }

        /// <summary>
        /// The Spotify ID for the artist.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Images of the artist in various sizes, widest first.
        /// </summary>
        [JsonProperty(PropertyName = "images")]
        public Image[] Images { get; set; }

        /// <summary>
        /// The name of the artist.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }


        /// <summary>
        /// The popularity of the artist. The value will be between 0 and 100, with 100 being the most popular. The artist’s popularity is calculated from the popularity of all the artist’s tracks.
        /// </summary>
        [JsonProperty(PropertyName = "popularity")]
        public int Popularity { get; set; }

        /// <summary>
        /// The object type.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// The Spotify URI for the artist.
        /// </summary>
        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }

    
    }
    public class ExternalUrls
    {
        /// <summary>
        /// The Spotify URL for the object.
        /// </summary>
        [JsonProperty(PropertyName = "spotify")]
        public string Spotify { get; set; }
    }

    public class Followers
    {
        /// <summary>
        /// This will always be set to null, as the Web API does not support it at the moment.
        /// </summary>
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        /// <summary>
        /// The total number of followers.
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }
    }
    public class Image
    {
        /// <summary>
        /// The image height in pixels.	
        /// </summary>
        [JsonProperty(PropertyName = "height")]
        public int Height { get; set; }

        /// <summary>
        /// The source URL of the image.
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// The image width in pixels.
        /// </summary>
        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }
    }

}
