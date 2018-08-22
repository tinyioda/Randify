using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Browser.Http;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Randify.App.Delegates;
using Randify.App.Models;
using Randify.App.Models.SpotifyModel;

namespace Randify.App.Services
{
    public class SpotifyService
    {
        /// <summary>
        /// 
        /// </summary>
        public static event SpotifyWebPlayerChange SpotifyWebPlayerChanged;

        /// <summary>
        /// 
        /// </summary>
        private Stopwatch _stopwatch;

        /// <summary>
        /// 
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<SpotifyService> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public SpotifyService(HttpClient client, ILogger<SpotifyService> logger)
        {
            _client = client;
            _logger = logger;
            _stopwatch = new Stopwatch();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task EnableSpotifyPlayer(AuthenticationToken token)
        {
            if (token == null)
                return;

            try
            {
                _logger.LogInformation("EnableSpotifyPlayer: " + await JSRuntime.Current.InvokeAsync<bool>("enableSpotifyPlayer", token.AccessToken));                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Uri"></param>
        /// <returns></returns>
        public async Task Play(string Uri)
        {
            try
            {
                _logger.LogInformation("Play: " + await JSRuntime.Current.InvokeAsync<string>("play", Uri));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task TogglePlay()
        {
            try
            {
                _logger.LogInformation("TogglePlay: " + await JSRuntime.Current.InvokeAsync<bool>("togglePlay"));
            }
            catch (Exception ex)
            { 
                _logger.LogError(ex, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        public static void PlayerStateChange(string json)
        {
            try
            {
                var state = WebPlaybackState.ToPOCOFromJSON(json);

                if (SpotifyWebPlayerChanged != null)
                    SpotifyWebPlayerChanged(state);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Get detailed profile information about the current user (including the current user’s username).
        /// </summary>
        /// <param name="userId"></param>
        public async Task<User> GetCurrentUserProfile(AuthenticationToken token)
        {
            try
            {
                _stopwatch.Reset();
                _stopwatch.Start();

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
                var response = await _client.GetAsync("https://api.spotify.com/v1/me");

                _logger.LogInformation("Response for GetCurrentUserProfile: " + response.StatusCode);
                
                var obj = Microsoft.JSInterop.Json.Deserialize<user>(await response.Content.ReadAsStringAsync());

                _stopwatch.Stop();
                _logger.LogInformation("GetCurrentUserProfile: " + _stopwatch.Elapsed.Seconds + "(s)");

                return obj.ToPOCO();
            }
            catch (Exception ex)
            { 
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Page<Playlist>> GetPlaylists(User user, AuthenticationToken token)
        {
            _stopwatch.Reset();
            _stopwatch.Start();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            var response = await _client.GetAsync("https://api.spotify.com/v1/users/" + user.Id + "/playlists");

            _logger.LogInformation("Response for GetPlaylists: " + response.StatusCode);

            var data = await response.Content.ReadAsStringAsync();
            var obj = Microsoft.JSInterop.Json.Deserialize<page<playlist>>(data);

            _stopwatch.Stop();
            _logger.LogInformation("GetPlaylists: " + _stopwatch.Elapsed.Seconds + "(s)");

            return obj.ToPOCO<Playlist>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Page<PlaylistTrack>> GetPlaylistTracks(User user, AuthenticationToken token, Playlist playlist)
        {
            _stopwatch.Reset();
            _stopwatch.Start();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            var response = await _client.GetAsync("https://api.spotify.com/v1/users/" + user.Id + "/playlists/" + playlist.Id + "/tracks?limit=100"); // + (new Random()).Next(11, 25));

            _logger.LogInformation("Response for GetPlaylistTracks: " + response.StatusCode);

            var data = await response.Content.ReadAsStringAsync();
            var obj = Microsoft.JSInterop.Json.Deserialize<page<playlisttrack>>(data);

            _stopwatch.Stop();
            _logger.LogInformation("GetPlaylistTracks: " + _stopwatch.Elapsed.Seconds + "(s)");

            return obj.ToPOCO<PlaylistTrack>();
        }

        /// <summary>
        /// Add one or more tracks to a user’s playlist.
        /// </summary>
        /// <param name="tracks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task AddTracksToPlaylist(User user, AuthenticationToken token, Playlist playlist, List<Track> tracks)
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

            var content = new StringContent(trackUris);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var response = await _client.PostAsync("https://api.spotify.com/v1/users/" + user.Id + "/playlists/" + playlist.Id + "/tracks", content);

            _logger.LogInformation("Response for AddTracksToPlaylist: " + response.StatusCode);
            _stopwatch.Stop();
            _logger.LogInformation("AddTracksToPlaylist: " + _stopwatch.Elapsed.Seconds + "(s)");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="tracks"></param>
        /// <returns></returns>
        public async Task RemoveTracksFromPlaylist(User user, AuthenticationToken token, Playlist playlist, List<Track> tracks)
        {
            if (tracks.Count > 100)
                throw new Exception("Can only remove 100 tracks at a time.");

            var trackUris = "{ \"tracks\":[";
            foreach (var track in tracks)
            {
                trackUris += "{\"uri\": \"spotify:track:" + track.Id + "\"},";
            }
            trackUris = trackUris.Trim(',');
            trackUris += "]}";

            _stopwatch.Reset();
            _stopwatch.Start();

            var request = new HttpRequestMessage
            {
                Content = new StringContent(trackUris, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri("https://api.spotify.com/v1/users/" + user.Id + "/playlists/" + playlist.Id + "/tracks")
            };

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var response = await _client.SendAsync(request);

            _logger.LogInformation("Response for RemoveTracksFromPlaylist: " + response.StatusCode);
            _stopwatch.Stop();
            _logger.LogInformation("RemoveTracksFromPlaylist: " + _stopwatch.Elapsed.Seconds + "(s)");
        }

        /// <summary>
        /// If this object has a Next page get it
        /// else
        /// throw new Exception("Next page does not exist.");
        /// </summary>
        /// <returns></returns>
        public async Task<Page<T>> GetNextPage<T>(Page<T> page, AuthenticationToken token)
        {
            _stopwatch.Reset();
            _stopwatch.Start();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            if (!page.HasNextPage)
                throw new Exception("Next page does not exist.");

            if (typeof(T) == typeof(Track))
            {
                var response = await _client.GetAsync(page.Next);

                _logger.LogInformation("Response for GetNextPage: " + response.StatusCode);

                var data = await response.Content.ReadAsStringAsync();
                var obj = Microsoft.JSInterop.Json.Deserialize<page<track>>(data);

                _stopwatch.Stop();
                _logger.LogInformation("GetNextPage: " + _stopwatch.Elapsed.Seconds + "(s)");

                return obj.ToPOCO<T>();
            }
            else if (typeof(T) == typeof(Playlist))
            {
                var response = await _client.GetAsync(page.Next);

                _logger.LogInformation("Response for GetNextPage: " + response.StatusCode);

                var obj = Microsoft.JSInterop.Json.Deserialize<page<playlist>>(await response.Content.ReadAsStringAsync());

                _stopwatch.Stop();
                _logger.LogInformation("GetNextPage: " + _stopwatch.Elapsed.Seconds + "(s)");

                return obj.ToPOCO<T>();
            }
            else if (typeof(T) == typeof(Artist))
            {
                var response = await _client.GetAsync(page.Next);

                _logger.LogInformation("Response for GetNextPage: " + response.StatusCode);

                var obj = Microsoft.JSInterop.Json.Deserialize<page<artist>>(await response.Content.ReadAsStringAsync());

                _stopwatch.Stop();
                _logger.LogInformation("GetNextPage: " + _stopwatch.Elapsed.Seconds + "(s)");

                return obj.ToPOCO<T>();
            }
            else if (typeof(T) == typeof(Album))
            {
                var response = await _client.GetAsync(page.Next);

                _logger.LogInformation("Response for GetNextPage: " + response.StatusCode);

                var obj = Microsoft.JSInterop.Json.Deserialize<page<album>>(await response.Content.ReadAsStringAsync());

                _stopwatch.Stop();
                _logger.LogInformation("GetNextPage: " + _stopwatch.Elapsed.Seconds + "(s)");

                return obj.ToPOCO<T>();
            }
            else if (typeof(T) == typeof(PlaylistTrack))
            {
                var response = await _client.GetAsync(page.Next);

                _logger.LogInformation("Response for GetNextPage: " + response.StatusCode);

                var data = await response.Content.ReadAsStringAsync();
                var obj = Microsoft.JSInterop.Json.Deserialize<page<playlisttrack>>(data);

                _stopwatch.Stop();
                _logger.LogInformation("GetNextPage: " + _stopwatch.Elapsed.Seconds + "(s)");

                return obj.ToPOCO<T>();
            }

            return null;
        }

        /// <summary>
        /// If this object has a Previous page get it
        /// else
        /// throw new Exception("Previous page does not exist.");
        /// </summary>
        /// <returns></returns>
        public async Task<Page<T>> GetPreviousPage<T>(Page<T> page, AuthenticationToken token)
        {
            _stopwatch.Reset();
            _stopwatch.Start();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            if (!page.HasPreviousPage)
                throw new Exception("Previous page does not exist.");

            if (typeof(T) == typeof(Track))
            {
                var response = await _client.GetAsync(page.Next);

                _logger.LogInformation("Response for GetPreviousPage: " + response.StatusCode);

                var obj = Microsoft.JSInterop.Json.Deserialize<page<track>>(await response.Content.ReadAsStringAsync());

                _stopwatch.Stop();
                _logger.LogInformation("GetPreviousPage: " + _stopwatch.Elapsed.Seconds + "(s)");

                return obj.ToPOCO<T>();
            }
            else if (typeof(T) == typeof(Playlist))
            {
                var response = await _client.GetAsync(page.Next);

                _logger.LogInformation("Response for GetPreviousPage: " + response.StatusCode);

                var obj = Microsoft.JSInterop.Json.Deserialize<page<playlist>>(await response.Content.ReadAsStringAsync());

                _stopwatch.Stop();
                _logger.LogInformation("GetPreviousPage: " + _stopwatch.Elapsed.Seconds + "(s)");

                return obj.ToPOCO<T>();
            }
            else if (typeof(T) == typeof(Artist))
            {
                var response = await _client.GetAsync(page.Next);

                _logger.LogInformation("Response for GetPreviousPage: " + response.StatusCode);

                var obj = Microsoft.JSInterop.Json.Deserialize<page<artist>>(await response.Content.ReadAsStringAsync());

                _stopwatch.Stop();
                _logger.LogInformation("GetPreviousPage: " + _stopwatch.Elapsed.Seconds + "(s)");

                return obj.ToPOCO<T>();
            }
            else if (typeof(T) == typeof(Album))
            {
                var response = await _client.GetAsync(page.Next);

                _logger.LogInformation("Response for GetPreviousPage: " + response.StatusCode);

                var obj = Microsoft.JSInterop.Json.Deserialize<page<album>>(await response.Content.ReadAsStringAsync());

                _stopwatch.Stop();
                _logger.LogInformation("GetPreviousPage: " + _stopwatch.Elapsed.Seconds + "(s)");

                return obj.ToPOCO<T>();
            }
            else if (typeof(T) == typeof(PlaylistTrack))
            {
                var response = await _client.GetAsync(page.Next);

                _logger.LogInformation("Response for GetPreviousPage: " + response.StatusCode);

                var obj = Microsoft.JSInterop.Json.Deserialize<page<playlisttrack>>(await response.Content.ReadAsStringAsync());

                _stopwatch.Stop();
                _logger.LogInformation("GetPreviousPage: " + _stopwatch.Elapsed.Seconds + "(s)");

                return obj.ToPOCO<T>();
            }

            return null;
        }
    }
}
