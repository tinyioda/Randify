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
    public class Browse
    {
        /// <summary>
        /// 
        /// </summary>
        private static SpotifyWebApi.HttpHelper HttpHelper { get; set; } = new HttpHelper();

        /// <summary>
        /// Get a list of Spotify featured playlists (shown, for example, on a Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="token"></param>
        /// <param name="locale"></param>
        /// <param name="country"></param>
        /// <param name="timestamp">A timestamp in ISO 8601 format: yyyy-MM-ddTHH:mm:ss.</param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async static Task<Page<Playlist>> GetFeaturedPlaylists(AuthenticationToken token, string locale = "", string country = "", string timestamp = "", int limit = 20, int offset = 0)
        {
            var queryString = "https://api.spotify.com/v1/browse/featured-playlists?";

            queryString += "limit=" + limit;
            queryString += "&offset=" + offset;

            if (timestamp != string.Empty)
                queryString += "&timestamp=" + timestamp;

            if (locale != string.Empty)
                queryString += "&locale=" + locale;

            if (country != string.Empty)
                queryString += "&country=" + country;
            
            var json = await HttpHelper.Get(queryString, token);
            var obj = JsonConvert.DeserializeObject<featuredplaylistssearchresult>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return obj.playlists.ToPOCO<Playlist>();
        }

        /// <summary>
        /// Get a list of new album releases featured in Spotify (shown, for example, on a Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="token"></param>
        /// <param name="country"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async static Task<Page<Album>> GetNewReleases(AuthenticationToken token, string country = "", int limit = 20, int offset = 0)
        {
            var queryString = "https://api.spotify.com/v1/browse/new-releases?";

            queryString += "limit=" + limit;
            queryString += "&offset=" + offset;

            if (country != string.Empty)
                queryString += "&country=" + country;

            var json = await HttpHelper.Get(queryString, token);
            var obj = JsonConvert.DeserializeObject<albumsearchresult>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return obj.albums.ToPOCO<Album>();
        }
    }
}
