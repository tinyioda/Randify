using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Randify.App.Models
{
    public class Artist
    {
        /// <summary>
        /// Known external URLs for this artist.
        /// </summary>
        public ExternalUrl ExternalUrl { get; set; }

        /// <summary>
        /// Information about the followers of the artist.
        /// </summary>
        public Followers Followers { get; set; }

        /// <summary>
        /// A list of the genres the artist is associated with. For example: "Prog Rock", "Post-Grunge". (If not yet classified, the array is empty.) 
        /// </summary>
        public List<string> Genres { get; set; }

        /// <summary>
        /// A link to the Web API endpoint providing full details of the artist.
        /// </summary>
        public string HREF { get; set; }

        /// <summary>
        /// The Spotify ID for the artist. 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Images of the artist in various sizes, widest first.
        /// </summary>
        public List<Image> Images { get; set; }

        /// <summary>
        /// The name of the artist 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The popularity of the artist. The value will be between 0 and 100, with 100 being the most popular.
        /// </summary>
        public int Popularity { get; set; }

        /// <summary>
        /// The object type: "artist"
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The Spotify URI for the artist.
        /// </summary>
        public string Uri { get; set; }
        
        /// <summary>
        /// default constructor
        /// </summary>
        public Artist()
        {
            this.ExternalUrl = null;
            this.Genres = new List<string>();
            this.HREF = null;
            this.Id = null;
            this.Images = new List<Image>();
            this.Name = null;
            this.Popularity = 0;
            this.Type = null;
            this.Uri = null;
        }
    }
}
