using System;


namespace SpotifyProxyAPI.Models
{
    /// <summary>
    /// Database settings parameters
    /// </summary>
    public class DatabaseSettings
    {
        /// <summary>
        /// Name of the database
        /// </summary>
        public String DatabaseName { get; set; }

        /// <summary>
        /// Collection name used
        /// </summary>
        public String CollectionName { get; set; }

        /// <summary>
        /// Connection String to connect to the database
        /// </summary>
        public String ConnectionString { get; set; }
    }
}
