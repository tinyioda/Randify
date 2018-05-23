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
    public class User : BaseModel
    {
        /// <summary>
        /// The country of the user, as set in the user's account profile. An ISO 3166-1 alpha-2 country code. 
        /// This field is only available when the current user has granted access to the user-read-private scope.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The name displayed on the user's profile. This field is only available when the 
        /// current user has granted access to the user-read-private scope.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The user's email address. This field is only available when the current user has granted access to the user-read-email scope.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Known external URLs for this user.
        /// </summary>
        public ExternalUrl ExternalUrl { get; set; }

        /// <summary>
        /// A link to the Web API endpoint for this user.
        /// </summary>
        public string HREF { get; set; }

        /// <summary>
        ///  The Spotify user ID for the user.  
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The user's profile image. This field is only available when the current user has granted access to the user-read-private scope.
        /// </summary>
        public List<Image> Images { get; set; }

        /// <summary>
        /// The user's Spotify subscription level: "premium", "free", etc. This field is only available when 
        /// the current user has granted access to the user-read-private scope.
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// The object type: "user"  
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The Spotify URI for the user.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static SpotifyWebApi.HttpHelper HttpHelper { get; set; } = new HttpHelper();

        /// <summary>
        /// Default constructor
        /// </summary>
        public User()
        {
            this.Country = null;
            this.DisplayName = null;
            this.EmailAddress = null;
            this.ExternalUrl = null;
            this.HREF = null;
            this.Id = null;
            this.Images = new List<Image>();
            this.Product = null;
            this.Type = null;
            this.Uri = null;
        }
        
        /// <summary>
        /// Get public profile information about a Spotify user.
        /// </summary>
        /// <param name="userId"></param>
        public async static Task<User> GetUser(string userId)
        {
            var json = await HttpHelper.Get("https://api.spotify.com/v1/users/" + userId);
                        
            var obj = JsonConvert.DeserializeObject<user>(json);

            return obj.ToPOCO();
        }

        /// <summary>
        /// Get detailed profile information about the current user (including the current user’s username).
        /// </summary>
        /// <param name="userId"></param>
        public async static Task<User> GetCurrentUserProfile(AuthenticationToken token)
        {
            var obj = await HttpHelper.GetJsonAsync<user>("https://api.spotify.com/v1/me", token);
            
            return obj.ToPOCO();
        }

        /// <summary>
        /// Get a list of the playlists owned by this Spotify user.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Page<Playlist>> GetPlaylists(AuthenticationToken token)
        {
            return await Playlist.GetUsersPlaylists(this.Id, token);
        }

        /// <summary>
        /// Get a list of the songs saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<Page<Track>> GetUsersSavedTracks(AuthenticationToken token, int limit = 20, int offset = 0)
        {
            var json = await HttpHelper.Get("https://api.spotify.com/v1/me/tracks?limit=" + limit + "&offset=" + offset, token);            
            var obj = JsonConvert.DeserializeObject<page<savedtrack>>(json);

            return obj.ToPOCO<Track>();
        }

        /// <summary>
        /// Get a list of the songs saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Page<Track>> GetSavedTracks(AuthenticationToken token, int limit = 20, int offset = 0)
        {
            return await GetUsersSavedTracks(token, limit, offset);
        }

        /// <summary>
        /// Save one or more tracks to the current user’s “Your Music” library.
        /// </summary>
        /// <param name="tracks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task SaveTracks(List<string> trackIds, AuthenticationToken token)
        {
            string tracksUri = CreateCommaSeperatedList(trackIds);
            var json = await HttpHelper.Put("https://api.spotify.com/v1/me/tracks?ids=" + tracksUri, token);
        }

        /// <summary>
        /// Remove one or more tracks from the current user’s “Your Music” library.
        /// </summary>
        /// <param name="tracks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task DeleteTracks(List<string> trackIds, AuthenticationToken token)
        {
            string tracksUri = CreateCommaSeperatedList(trackIds);
            var json = await HttpHelper.Delete("https://api.spotify.com/v1/me/tracks?ids=" + tracksUri, token);
        }

        /// <summary>
        /// Remove one or more tracks from the current user’s “Your Music” library.
        /// </summary>
        /// <param name="tracks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> AreSaved(List<string> trackIds, AuthenticationToken token)
        {
            string tracksUri = CreateCommaSeperatedList(trackIds);
            var json = await HttpHelper.Get("https://api.spotify.com/v1/me/tracks/contains?ids=" + tracksUri, token);
            var obj = JsonConvert.DeserializeObject(json);

            return Convert.ToBoolean(obj.ToString().Replace("{", string.Empty).Replace("[", string.Empty).Replace("}", string.Empty).Replace("]", string.Empty).Trim());
        }

        /// <summary>
        /// Check if a is already saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="tracks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> IsSaved(string trackId, AuthenticationToken token)
        {
            List<string> trackIds = new List<string>();
            trackIds.Add(trackId);

            return await AreSaved(trackIds, token);
        }

        /// <summary>
        /// Get information about a user’s available devices.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<Device>> GetUsersAvailableDevices(AuthenticationToken token)
        {
            return await Player.GetUsersAvailableDevices(token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Artist>> GetFollowedArtists(AuthenticationToken token, int limit = 20, string after = "")
        {
            var uri = "https://api.spotify.com/v1/me/following?type=artist&limit=" + limit;
            if (after != string.Empty)
                uri += "&after=" + after;

            var json = await HttpHelper.Get(uri, token, true);
            var obj = JsonConvert.DeserializeObject<Dictionary<string, cursorpage<artist>>>(json);

            var artists = new List<Artist>();

            foreach (var o in obj.Values)
            {
                foreach (artist artist in o.items)
                {
                    artists.Add(artist.ToPOCO());
                }
            }

            return artists;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Artist>> GetUsersTopArtists(AuthenticationToken token, int limit = 20, int offset = 0, TimeRanges timeRange = TimeRanges.MediumTerm)
        {
            var uri = "https://api.spotify.com/v1/me/top/artists?limit=" + limit + "&offset=" + offset;

            switch (timeRange)
            {
                case TimeRanges.LongTerm:
                    uri += "&time_range=long_term";
                    break;

                case TimeRanges.ShortTerm:
                    uri += "&time_range=short_term";
                    break;

                default:
                    uri += "&time_range=medium_term";
                    break;
            }

            var json = await HttpHelper.Get(uri, token, true);
            page<artist> obj = JsonConvert.DeserializeObject<page<artist>>(json);

            var artists = new List<Artist>();

            foreach (artist artist in obj.items)
            {
                artists.Add(artist.ToPOCO());
            }

            return artists;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="timeRange"></param>
        /// <param name="getFullObjects"></param>
        /// <returns></returns>
        public async Task<List<Track>> GetUsersTopTracks(AuthenticationToken token, int limit = 20, int offset = 0, TimeRanges timeRange = TimeRanges.MediumTerm, bool getFullObjects = false)
        {
            var uri = "https://api.spotify.com/v1/me/top/tracks?limit=" + limit + "&offset=" + offset;

            switch (timeRange)
            {
                case TimeRanges.LongTerm:
                    uri += "&time_range=long_term";
                    break;

                case TimeRanges.ShortTerm:
                    uri += "&time_range=short_term";
                    break;

                default:
                    uri += "&time_range=medium_term";
                    break;
            }

            var json = await HttpHelper.Get(uri, token, true);
            var obj = JsonConvert.DeserializeObject<page<track>>(json);

            var tracks = new List<Track>();

            foreach (track track in obj.items)
            {
                tracks.Add(track.ToPOCO());
            }

            if (getFullObjects)
            {
                foreach (var track in tracks)
                {
                    var fullObjects = new List<Artist>();
                    foreach (var artist in track.Artists)
                    {
                        fullObjects.Add(await Artist.GetArtist(token, artist.Id));
                    }
                    track.Artists = fullObjects;
                }

                foreach (var track in tracks)
                {
                    if (track.Album.Id != null)
                        track.Album = await Album.GetAlbum(token, track.Album.Id);
                }
            }

            return tracks;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="getFullObjects"></param>
        /// <returns></returns>
        public async Task<List<Album>> GetUsersSavedAlbums(AuthenticationToken token, int limit = 20, int offset = 0, string market = "US", bool getFullObjects = false)
        {
            var uri = "https://api.spotify.com/v1/me/albums?limit=" + limit + "&offset=" + offset + "&market=" + market;

            var json = await HttpHelper.Get(uri, token, true);
            var obj = JsonConvert.DeserializeObject<page<savedalbum>>(json);

            var albums = new List<Album>();

            foreach (var item in obj.items)
            {
                albums.Add(item.album.ToPOCO());
            }

            return albums;
        }
    }
}
