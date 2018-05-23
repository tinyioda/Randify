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
    public class Playlist : BaseModel
    {
        /// <summary>
        /// true if the owner allows other users to modify the playlist. Note: only non-collaborative playlists are currently returned by the Web API.
        /// </summary>
        public bool Collaborative { get; set; }

        /// <summary>
        /// The playlist description. Only returned for modified, verified playlists, otherwise null.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Known external URLs for this playlist.
        /// </summary>
        public ExternalUrl ExternalUrl { get; set; }

        /// <summary>
        /// Information about the followers of the playlist. 
        /// </summary>
        public Followers Followers { get; set; }

        /// <summary>
        /// A link to the Web API endpoint providing full details of the playlist.
        /// </summary>
        public string HREF { get; set; }

        /// <summary>
        /// The Spotify ID for the playlist. 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The playlist image. Note that this field is only returned for modified, verified playlists, otherwise the 
        /// array is empty. If returned, the source URL for the image (url) is temporary and will expire in less than a day.
        /// </summary>
        public List<Image> Images { get; set; } = new List<Image>();

        /// <summary>
        /// The name of the playlist.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The user who owns the playlist
        /// </summary>
        public User Owner { get; set; }

        /// <summary>
        /// true if the playlist is not marked as secret. 
        /// </summary>
        public bool Public { get; set; }

        /// <summary>
        /// Information about the tracks of the playlist. 
        /// </summary>
        public Page<PlaylistTrack> Tracks { get; set; }

        /// <summary>
        /// The object type: "playlist"
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
        /// Get a list of the playlists owned by a Spotify user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<Page<Playlist>> GetUsersPlaylists(User user, AuthenticationToken token)
        {
            return await GetUsersPlaylists(user.Id, token);
        }

        /// <summary>
        /// Get a list of the playlists owned by a Spotify user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<Page<Playlist>> GetUsersPlaylists(string userId, AuthenticationToken token)
        {
            var obj = await HttpHelper.GetJsonAsync<page<playlist>>("https://api.spotify.com/v1/users/" + userId + "/playlists", token);            
            return obj.ToPOCO<Playlist>(); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<Playlist> GetPlaylist(User user, string playlistId, AuthenticationToken token)
        {
            return await GetPlaylist(user.Id, playlistId, token);
        }

        /// <summary>
        /// Get a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<Playlist> GetPlaylist(string userId, string playlistId, AuthenticationToken token)
        {
            var json = await HttpHelper.Get("https://api.spotify.com/v1/users/" + userId + "/playlists/" + playlistId, token);
            var obj = JsonConvert.DeserializeObject<playlist>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
                });

            return obj.ToPOCO(); 
        }

        /// <summary>
        /// Get full details of the tracks of a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<Page<PlaylistTrack>> GetPlaylistTracks(string userId, string playlistId, AuthenticationToken token)
        {
            var obj = await HttpHelper.GetJsonAsync<page<playlisttrack>>("https://api.spotify.com/v1/users/" + userId + "/playlists/" + playlistId + "/tracks?limit=20", token);
            return obj.ToPOCO<PlaylistTrack>();
        }

        /// <summary>
        /// Get full details of the tracks of this playlist
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Page<PlaylistTrack>> GetPlaylistTracks(AuthenticationToken token)
        {
            return await GetPlaylistTracks(this.Owner.Id, this.Id, token);
        }

        /// <summary>
        /// Add a track to a user’s playlist.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task AddTrack(Track track, AuthenticationToken token)
        {
            var tracks = new List<Track>();
            tracks.Add(track);

            await AddTracks(tracks, token);
        }

        /// <summary>
        /// Add one or more tracks to a user’s playlist.
        /// </summary>
        /// <param name="tracks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task AddTracks(List<Track> tracks, AuthenticationToken token)
        {
            if (tracks.Count > 100)
                throw new Exception("Can only add 100 tracks at a time.");

            var trackUris = "{ \"uris\":[";

            foreach (var track in tracks)
            {
                trackUris += "\"spotify:track:" + track.Id + "\",";
            }

            trackUris = trackUris.Trim(',');

            trackUris += "]}";
            
            Console.WriteLine(trackUris);

            await HttpHelper.Post("https://api.spotify.com/v1/users/" + this.Owner.Id + "/playlists/" + this.Id + "/tracks", token, trackUris);
        }

        /// <summary>
        /// Create a playlist for a Spotify user. (The playlist will be empty until you add tracks.)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="isPublic"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<Playlist> CreatePlaylist(string userId, string name, bool isPublic, AuthenticationToken token)
        {
            dynamic newObject = new System.Dynamic.ExpandoObject();
            newObject.name = name;
            newObject.@public = isPublic;

            string jsonInput = JsonConvert.SerializeObject(newObject);
            var json = await HttpHelper.Post("https://api.spotify.com/v1/users/" + userId + "/playlists", token, jsonInput);
            var obj = JsonConvert.DeserializeObject<playlist>(json);

            return obj.ToPOCO(); 
        }

        /// <summary>
        /// Change a playlist’s name and public/private state. (The user must, of course, own the playlist.)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="isPublic"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task UpdateUsersPlaylist(string userId, string playlistId, string name, bool isPublic, AuthenticationToken token)
        {
            dynamic newObject = new System.Dynamic.ExpandoObject();
            newObject.name = name;
            newObject.@public = isPublic;

            string jsonInput = JsonConvert.SerializeObject(newObject);
            var json = await HttpHelper.Put("https://api.spotify.com/v1/users/" + userId + "/playlists/" + playlistId, token, jsonInput);
        }        

        /// <summary>
        /// Change a playlist’s name and public/private state. (The user must, of course, own the playlist.)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="isPublic"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task UpdatePlaylist(string name, bool isPublic, AuthenticationToken token)
        {
            await UpdateUsersPlaylist(this.Owner.Id, this.Id, name, isPublic, token);
        }

        /// <summary>
        /// Remove one or more tracks from a user’s playlist.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="playlistId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task RemoveTracksFromPlaylist(string userId, AuthenticationToken token, List<Track> tracks)
        {
            var trackUris = "{ \"tracks\":[";

            foreach (var track in tracks)
            {
                trackUris += "{\"uri\": \"spotify:track:" + track.Id + "\"},";
            }

            trackUris = trackUris.Trim(',');

            trackUris += "]}";

            Console.WriteLine(trackUris);

            await HttpHelper.Delete("https://api.spotify.com/v1/users/" + this.Owner.Id + "/playlists/" + this.Id + "/tracks", token, trackUris);
        }

        /// <summary>
        /// Replace all the tracks in a playlist, overwriting its existing tracks. This powerful request can be useful for replacing tracks, re-ordering existing tracks, or clearing the playlist.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="playlistId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task ReplacePlaylistTracks(string userId, string playlistId, AuthenticationToken token, List<Track> tracks)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a list of featured playlists
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
            return await Browse.GetFeaturedPlaylists(token, locale, country, timestamp, limit, offset);
        }
    }
}
