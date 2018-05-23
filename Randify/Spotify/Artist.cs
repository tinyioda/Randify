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
    public class Artist : BaseModel
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
        /// 
        /// </summary>
        private static SpotifyWebApi.HttpHelper HttpHelper { get; set; } = new HttpHelper();

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

        /// <summary>
        /// Search for an artist
        /// </summary>
        /// <param name="albumName"></param>
        /// <param name="artistName"></param>
        /// <param name="year"></param>
        /// <param name="genre"></param>
        /// <param name="upc">Universal Product Code</param>
        /// <param name="isrc">International Standard Recording Code</param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static async Task<Page<Artist>> Search(string artistName,
            string year = "",
            string genre = "",
            string upc = "",
            string isrc = "",
            int limit = 20,
            int offset = 0)
        {
            string queryString = "https://api.spotify.com/v1/search?q=artist:" + artistName.Replace(" ", "%20");

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
            queryString += "&type=artist";

            var json = await HttpHelper.Get(queryString);

            var obj = JsonConvert.DeserializeObject<artistsearchresult>(json);

            return obj.artists.ToPOCO<Artist>();
        }

        /// <summary>
        /// Get an artist
        /// </summary>
        /// <returns></returns>
        public static async Task<Artist> GetArtist(AuthenticationToken token, string artistId)
        {
            var json = await HttpHelper.Get("https://api.spotify.com/v1/artists/" + artistId, token, true);
            var obj = JsonConvert.DeserializeObject<artist>(json);

            return obj.ToPOCO();
        }

        /// <summary>
        ///  Get several artists  
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Artist>> GetArtists(List<string> artistIds)
        {
            var json = await HttpHelper.Get("https://api.spotify.com/v1/artists/?ids=" + CreateCommaSeperatedList(artistIds));
            var obj = JsonConvert.DeserializeObject<artistarray>(json);

            List<Artist> artists = new List<Artist>();

            foreach (var item in obj.artists)
                artists.Add(item.ToPOCO());

            return artists;
        }
        
        /// <summary>
        /// Get this artist's albums
        /// </summary>
        /// <returns></returns>
        public async Task<Page<Album>> GetAlbums()
        {
            return await Album.GetArtistAlbums(this.Id);
        }

        /// <summary>
        /// Get an artist's top tracks
        /// </summary>
        /// <param name="artistId"></param>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public static async Task<List<Track>> GetTopTracks(AuthenticationToken token, string artistId, string countryCode = "US")
        {
            var json = await HttpHelper.Get("https://api.spotify.com/v1/artists/" + artistId + "/top-tracks?country=" + countryCode, token, true);
            var obj = JsonConvert.DeserializeObject<trackarray>(json);

            List<Track> tracks = new List<Track>();

            foreach (var item in obj.tracks)
                tracks.Add(item.ToPOCO());

            return tracks;
        }

        /// <summary>
        /// Get this artist's top tracks
        /// </summary>
        /// <returns></returns>
        public async Task<List<Track>> GetTopTracks(AuthenticationToken token, string countryCode = "US")
        {
            return await GetTopTracks(token, this.Id, countryCode);
        }

        /// <summary>
        /// Get an artist's related artists
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns></returns>
        public static async Task<List<Artist>> GetRelatedArtists(string artistId)
        {
            var json = await HttpHelper.Get("https://api.spotify.com/v1/artists/" + artistId + "/related-artists");
            var obj = JsonConvert.DeserializeObject<artistarray>(json);

            List<Artist> artists = new List<Artist>();
            foreach (var item in obj.artists)
                artists.Add(item.ToPOCO());

            return artists;
        }

        /// <summary>
        /// Get this artist's related artists
        /// </summary>
        /// <returns></returns>
        public async Task<List<Artist>> GetRelatedArtists()
        {
            return await GetRelatedArtists(this.Id);
        }
    }
}
