using Microsoft.AspNetCore.Blazor.Components;
using Newtonsoft.Json;
using SpotifyWebApi.SpotifyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyWebApi
{
    /// <summary>
    /// Spotify Track
    /// </summary>
    public class Track : BaseModel
    {
        /// <summary>
        /// The album on which the track appears. The album object includes a link in href to full information about the album. 
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// The artists who performed the track. Each artist object includes a link in href to more detailed information about the artist. 
        /// </summary>
        public List<Artist> Artists { get; set; }

        /// <summary>
        ///  A list of the countries in which the track can be played, identified by their ISO 3166-1 alpha-2 code.  
        /// </summary>
        public List<string> AvailableMarkets { get; set; }
        
        /// <summary>
        /// The disc number (usually 1 unless the album consists of more than one disc).  
        /// </summary>
        public int DiscNumber { get; set; }

        /// <summary>
        /// The track length in milliseconds.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Whether or not the track has explicit lyrics (true = yes it does; false = no it does not OR unknown).  
        /// </summary>
        public bool Explicit { get; set; }

        /// <summary>
        /// Known external IDs for the track.
        /// </summary>
        public ExternalId ExternalId { get; set; }

        /// <summary>
        /// Known external URLs for this track.
        /// </summary>
        public ExternalUrl ExternalUrl { get; set; }

        /// <summary>
        /// A link to the Web API endpoint providing full details of the track.
        /// </summary>
        public string HREF { get; set; }

        /// <summary>
        /// The Spotify ID for the track. 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the track. 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The popularity of the track. The value will be between 0 and 100, with 100 being the most popular. 
        /// </summary>
        public int Popularity { get; set; }

        /// <summary>
        /// A link to a 30 second preview (MP3 format) of the track.
        /// </summary>
        public string PreviewUrl { get; set; }

        /// <summary>
        /// The number of the track. If an album has several discs, the track number is the number on the specified disc.
        /// </summary>
        public int TrackNumber { get; set; }

        /// <summary>
        /// The object type: "track".
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The Spotify URI for the track.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static SpotifyWebApi.HttpHelper HttpHelper { get; set; } = new HttpHelper();

        /// <summary>
        /// default constructor
        /// </summary>
        public Track()
        {
            this.Album = null;
            this.Artists = new List<Artist>();
            this.AvailableMarkets = new List<string>();
            this.DiscNumber = 0;
            this.Duration = 0;
            this.Explicit = false;
            this.ExternalId = null;
            this.ExternalUrl = null;
            this.HREF = null;
            this.Id = null;
            this.Name = null;
            this.Popularity = 0;
            this.PreviewUrl = null;
            this.TrackNumber = 0;
            this.Type = null;
            this.Uri = null;
        }

        /// <summary>
        /// Search for a track
        /// </summary>
        /// <param name="trackName"></param>
        /// <param name="albumName"></param>
        /// <param name="artistName"></param>
        /// <param name="year"></param>
        /// <param name="genre"></param>
        /// <param name="upc"></param>
        /// <param name="isrc"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static async Task<Page<Track>> Search(string trackName,
            string albumName = "",
            string artistName = "",
            string year = "",
            string genre = "",
            string upc = "",
            string isrc = "",
            int limit = 20,
            int offset = 0)
        {
            string queryString = "https://api.spotify.com/v1/search?q=track:" + trackName.Replace(" ", "%20");

            if (albumName != "")
                queryString += "%20:album:" + albumName.Replace(" ", "%20");
            if (artistName != "")
                queryString += "%20:artist:" + artistName.Replace(" ", "%20");
            if (year != "")
                queryString += "%20:year:" + year.Replace(" ", "%20");
            if (genre != "")
                queryString += "%20:genre:" + genre.Replace(" ", "%20");
            if (upc != "")
                queryString += "%20:upc:" + upc.Replace(" ", "%20");
            if (isrc != "")
                queryString += "%20:isrc:" + isrc.Replace(" ", "%20");

            queryString += "&limit=" + limit;
            queryString += "&offset=" + offset;
            queryString += "&type=track";

            var json = await HttpHelper.Get(queryString);
            var obj = JsonConvert.DeserializeObject<tracksearchresult>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return obj.tracks.ToPOCO<Track>();
        }

        /// <summary>
        /// Get a track
        /// </summary>
        /// <param name="trackId"></param>
        /// <returns></returns>
        public static async Task<Track> GetTrack(string trackId)
        {
            var json = await HttpHelper.Get("https://api.spotify.com/v1/tracks/" + trackId);
            var obj = JsonConvert.DeserializeObject<track>(json);

            return obj.ToPOCO();
        }

        /// <summary>
        /// Get several tracks
        /// </summary>
        /// <param name="trackIds"></param>
        /// <returns></returns>
        public static async Task<List<Track>> GetTracks(List<string> trackIds)
        {
            var json = await HttpHelper.Get("https://api.spotify.com/v1/tracks?ids=" + CreateCommaSeperatedList(trackIds));
            var obj = JsonConvert.DeserializeObject<trackarray>(json);

            List<Track> tracks = new List<Track>();
            foreach (var item in obj.tracks)
                tracks.Add(item.ToPOCO());

            return tracks;
        }

        /// <summary>
        /// Get an album's tracks
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public static async Task<Page<Track>> GetAlbumTracks(AuthenticationToken token,
            string albumId,
            int limit = 20,
            int offset = 0,
            string market = "US")
        {
            return await Album.GetAlbumTracks(token, albumId, limit, offset, market);
        }

        /// <summary>
        /// Get an artist's top tracks
        /// </summary>
        /// <param name="artistId"></param>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public static async Task<List<Track>> GetArtistTopTracks(string artistId, string countryCode = "US")
        {
            var json = await HttpHelper.Get("https://api.spotify.com/v1/artists/" + artistId + "/top-tracks?country=" + countryCode);
            var obj = JsonConvert.DeserializeObject<trackarray>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            List<Track> tracks = new List<Track>();
            foreach (var item in obj.tracks)
                tracks.Add(item.ToPOCO());

            return tracks;
        }
    }
}
