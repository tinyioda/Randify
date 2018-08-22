using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Randify.Models
{
    public class Album
    {
        /// <summary>
        /// The type of the album: one of "album", "single", or "compilation". 
        /// </summary>
        public AlbumType AlbumType { get; set; }

        /// <summary>
        /// The artists of the album. Each artist object includes a link in href to more detailed information about the artist.
        /// </summary>
        public List<Artist> Artists { get; set; }

        /// <summary>
        /// The markets in which the album is available: ISO 3166-1 alpha-2 country codes. Note that an album is considered available in a market when at least 1 of its tracks is available in that market.
        /// </summary>
        public List<string> AvailableMarkets { get; set; }

        /// <summary>
        /// The copyright statements of the album.
        /// </summary>
        public List<Copyright> Copyrights { get; set; }

        /// <summary>
        /// Known external IDs for the album.
        /// </summary>
        public ExternalId ExternalId { get; set; }

        /// <summary>
        /// Known external URLs for this album.
        /// </summary>
        public ExternalUrl ExternalUrl { get; set; }

        /// <summary>
        /// A list of the genres used to classify the album. For example: "Prog Rock", "Post-Grunge". (If not yet classified, the array is empty.) 
        /// </summary>
        public List<string> Genres { get; set; }

        /// <summary>
        /// A link to the Web API endpoint providing full details of the album.
        /// </summary>
        public string HREF { get; set; }

        /// <summary>
        /// A link to the Web API endpoint providing full details of the album.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The cover art for the album in various sizes, widest first.
        /// </summary>
        public List<Image> Images { get; set; }

        /// <summary>
        /// The label for the album.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The name of the album.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The popularity of the album. The value will be between 0 and 100, with 100 being the most popular. 
        /// </summary>
        public int Popularity { get; set; }

        /// <summary>
        /// The popularity of the album. The value will be between 0 and 100, with 100 being the most popular. 
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// The precision with which release_date value is known: "year", "month", or "day".
        /// </summary>
        public string ReleaseDatePrecision { get; set; }

        /// <summary>
        /// The tracks of the album.
        /// </summary>
        public Page<Track> Tracks { get; set; }

        /// <summary>
        /// The object type: "album"
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The Spotify URI for the album. 
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Album()
        {
            this.AlbumType = Randify.Models.AlbumType.Album;
            this.Artists = new List<Artist>();
            this.AvailableMarkets = new List<string>();
            this.ExternalId = null;
            this.ExternalUrl = null;
            this.Genres = new List<string>();
            this.HREF = null;
            this.Id = null;
            this.Images = new List<Image>();
            this.Name = null;
            this.Popularity = 0;
            this.ReleaseDate = DateTime.MinValue;
            this.ReleaseDatePrecision = null;
            this.Tracks = null;
            this.Type = null;
            this.Uri = null;
        }
    }
}
