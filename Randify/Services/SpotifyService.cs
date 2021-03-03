using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using Randify.Models;
using Randify.Models.SpotifyModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Randify.Services
{
	public class SpotifyService
	{
		private Stopwatch _stopwatch;

		private readonly HttpClient _client;

		private static string Limit = "100";

		public static readonly string SpotifyClientId = "07a41c900d2b407aa5defbceed492634";

		public static readonly string SpotifyClientSecret = "60a1e713b89a4125ab6433ce4c8fd97e";

#if DEBUG
		public string SpotifyLoginUrl
		{
			get;
			set;
		} = "https://accounts.spotify.com/authorize?response_type=token&client_id=07a41c900d2b407aa5defbceed492634&scope=user-read-email%20user-read-birthdate%20streaming%20playlist-read-private%20playlist-modify-private%20playlist-read-collaborative%20playlist-modify-public%20user-read-private%20user-modify-playback-state%20user-read-currently-playing%20user-read-playback-state%20user-follow-read%20user-library-modify%20user-top-read&redirect_uri=https://localhost:44370/Auth/SpotifyCallback";
#else
		public string SpotifyLoginUrl
		{
			get;
			set;
		} = "https://accounts.spotify.com/authorize?response_type=token&client_id=07a41c900d2b407aa5defbceed492634&scope=user-read-email%20user-read-birthdate%20streaming%20playlist-read-private%20playlist-modify-private%20playlist-read-collaborative%20playlist-modify-public%20user-read-private%20user-modify-playback-state%20user-read-currently-playing%20user-read-playback-state%20user-follow-read%20user-library-modify%20user-top-read&redirect_uri=https://randify.azurewebsites.net/Auth/SpotifyCallback";
#endif

		/// <summary>
		/// 
		/// </summary>
		/// <param name="client"></param>
		public SpotifyService(HttpClient client)
		{
			_client = client;
			_stopwatch = new Stopwatch();
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

				var data = await response.Content.ReadAsStringAsync();
				var obj = System.Text.Json.JsonSerializer.Deserialize<user>(data);

				_stopwatch.Stop();

				return obj.ToPOCO();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="token"></param>
		/// <param name="timeRage"></param>
		/// <returns></returns>
		public async Task<Page<Artist>> GetUsersTopArtists(AuthenticationToken token, int limit = 50, TimeRange timeRage = TimeRange.MediumTerm, Page<Artist> page = null)
		{
			_stopwatch.Reset();
			_stopwatch.Start();

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

			var url = "https://api.spotify.com/v1/me/top/artists?limit=" + limit;

			switch (timeRage)
			{
				case TimeRange.ShortTerm:
					url += "&time_range=short_term";
					break;
				case TimeRange.LongTerm:
					url += "&time_range=long_term";
					break;
				default:
					url += "&time_range=medium_term";
					break;
			}

			if (page != null && page.Next != null)
				url = page.Next;

			var response = await _client.GetAsync(url);

			var data = await response.Content.ReadAsStringAsync();
			var obj = System.Text.Json.JsonSerializer.Deserialize<page<artist>>(data);

			_stopwatch.Stop();

			return obj.ToPOCO<Artist>();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="token"></param>
		/// <param name="timeRage"></param>
		/// <returns></returns>
		public async Task<Page<Track>> GetUsersTopTracks(AuthenticationToken token, int limit = 50, TimeRange timeRage = TimeRange.MediumTerm, Page<Track> page = null)
		{
			_stopwatch.Reset();
			_stopwatch.Start();

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

			var url = "https://api.spotify.com/v1/me/top/tracks?limit=" + limit;

			switch (timeRage)
			{
				case TimeRange.ShortTerm:
					url += "&time_range=short_term";
					break;
				case TimeRange.LongTerm:
					url += "&time_range=long_term";
					break;
				default:
					url += "&time_range=medium_term";
					break;
			}

			if (page != null && page.Next != null)
				url = page.Next;

			var response = await _client.GetAsync(url);

			var data = await response.Content.ReadAsStringAsync();
			var obj = System.Text.Json.JsonSerializer.Deserialize<page<track>>(data);

			_stopwatch.Stop();

			return obj.ToPOCO<Track>();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="token"></param>
		/// <returns></returns>
		public async Task<CursorPage<Artist>> GetArtists(AuthenticationToken token, CursorPage<Artist> page = null)
		{
			_stopwatch.Reset();
			_stopwatch.Start();

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

			var request = "https://api.spotify.com/v1/me/following?type=artist&limit=50";

			if (page != null && page.Next != null)
				request = page.Next;

			var response = await _client.GetAsync(request);
			var data = (JObject)JObject.Parse(await response.Content.ReadAsStringAsync());
			var obj = System.Text.Json.JsonSerializer.Deserialize<cursorpage<artist>>(data.First.First.ToString());

			_stopwatch.Stop();

			return obj.ToPOCO<Artist>();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="artistId"></param>
		/// <returns></returns>
		public async Task<List<Track>> GetArtistTopTracks(string artistId, AuthenticationToken token)
		{
			_stopwatch.Reset();
			_stopwatch.Start();

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

			var request = "https://api.spotify.com/v1/artists/" + artistId + "/top-tracks?country=us";

			var response = await _client.GetAsync(request);
			var data = (JObject)JObject.Parse(await response.Content.ReadAsStringAsync());
			var obj = System.Text.Json.JsonSerializer.Deserialize<track[]>(data.First.First.ToString());

			var tracks = new List<Track>();

			foreach (var track in obj)
			{
				tracks.Add(track.ToPOCO());
			}

			_stopwatch.Stop();

			return tracks;
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

			var data = await response.Content.ReadAsStringAsync();
			var obj = System.Text.Json.JsonSerializer.Deserialize<page<playlist>>(data);

			_stopwatch.Stop();

			return obj.ToPOCO<Playlist>();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="token"></param>
		/// <param name="name"></param>
		/// <param name="description"></param>
		/// <param name="isPublic"></param>
		/// <returns></returns>
		public async Task<Playlist> CreatePlaylist(User user, AuthenticationToken token, string name, string description = "", bool isPublic = false)
		{
			_stopwatch.Reset();
			_stopwatch.Start();

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

			var request = new HttpRequestMessage(HttpMethod.Post, "https://api.spotify.com/v1/users/" + user.Id + "/playlists");
			request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(new { name = name, description = description, @public = isPublic }), Encoding.UTF8, "application/json");

			var response = await _client.SendAsync(request);

			var data = await response.Content.ReadAsStringAsync();
			var obj = System.Text.Json.JsonSerializer.Deserialize<playlist>(data);

			_stopwatch.Stop();

			return obj.ToPOCO();
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
			var response = await _client.GetAsync("https://api.spotify.com/v1/users/" + user.Id + "/playlists/" + playlist.Id + "/tracks?limit=" + Limit); // + (new Random()).Next(11, 25));

			var data = await response.Content.ReadAsStringAsync();
			var obj = System.Text.Json.JsonSerializer.Deserialize<page<playlisttrack>>(data);

			_stopwatch.Stop();

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

			_stopwatch.Stop();
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

			_stopwatch.Stop();
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

				var data = await response.Content.ReadAsStringAsync();
				var obj = System.Text.Json.JsonSerializer.Deserialize<page<track>>(data);

				_stopwatch.Stop();

				return obj.ToPOCO<T>();
			}
			else if (typeof(T) == typeof(Playlist))
			{
				var response = await _client.GetAsync(page.Next);

				var obj = System.Text.Json.JsonSerializer.Deserialize<page<playlist>>(await response.Content.ReadAsStringAsync());

				_stopwatch.Stop();

				return obj.ToPOCO<T>();
			}
			else if (typeof(T) == typeof(Artist))
			{
				var response = await _client.GetAsync(page.Next);

				var obj = System.Text.Json.JsonSerializer.Deserialize<page<artist>>(await response.Content.ReadAsStringAsync());

				_stopwatch.Stop();

				return obj.ToPOCO<T>();
			}
			else if (typeof(T) == typeof(Album))
			{
				var response = await _client.GetAsync(page.Next);

				var obj = System.Text.Json.JsonSerializer.Deserialize<page<album>>(await response.Content.ReadAsStringAsync());

				_stopwatch.Stop();

				return obj.ToPOCO<T>();
			}
			else if (typeof(T) == typeof(PlaylistTrack))
			{
				var response = await _client.GetAsync(page.Next);

				var data = await response.Content.ReadAsStringAsync();
				var obj = System.Text.Json.JsonSerializer.Deserialize<page<playlisttrack>>(data);

				_stopwatch.Stop();

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

				var obj = System.Text.Json.JsonSerializer.Deserialize<page<track>>(await response.Content.ReadAsStringAsync());

				_stopwatch.Stop();

				return obj.ToPOCO<T>();
			}
			else if (typeof(T) == typeof(Playlist))
			{
				var response = await _client.GetAsync(page.Next);

				var obj = System.Text.Json.JsonSerializer.Deserialize<page<playlist>>(await response.Content.ReadAsStringAsync());

				_stopwatch.Stop();

				return obj.ToPOCO<T>();
			}
			else if (typeof(T) == typeof(Artist))
			{
				var response = await _client.GetAsync(page.Next);

				var obj = System.Text.Json.JsonSerializer.Deserialize<page<artist>>(await response.Content.ReadAsStringAsync());

				_stopwatch.Stop();

				return obj.ToPOCO<T>();
			}
			else if (typeof(T) == typeof(Album))
			{
				var response = await _client.GetAsync(page.Next);

				var obj = System.Text.Json.JsonSerializer.Deserialize<page<album>>(await response.Content.ReadAsStringAsync());

				_stopwatch.Stop();

				return obj.ToPOCO<T>();
			}
			else if (typeof(T) == typeof(PlaylistTrack))
			{
				var response = await _client.GetAsync(page.Next);

				var obj = System.Text.Json.JsonSerializer.Deserialize<page<playlisttrack>>(await response.Content.ReadAsStringAsync());

				_stopwatch.Stop();

				return obj.ToPOCO<T>();
			}

			return null;
		}
	}
}