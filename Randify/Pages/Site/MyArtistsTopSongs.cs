using Microsoft.AspNetCore.Components;
using Randify.Models;
using Randify.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Randify.Pages.Site
{
	/// <summary>
	/// 
	/// </summary>
	public class MyArtistsTopSongs : BasePageAuthenticated
	{
		/// <summary>
		/// Is the page loaded or not
		/// </summary>
		public bool Loaded
		{
			get;
			set;
		}

		/// <summary>
		/// Show the playlist has been saved message
		/// </summary>
		public bool ShowPlaylistSaved
		{
			get;
			set;
		}

		/// <summary>
		/// A collection of favorited artists
		/// </summary>
		public List<Artist> Artists
		{
			get;
			set;
		} = new List<Artist>();

		/// <summary>
		/// A collection of tracks from the favorited artists
		/// </summary>
		public List<Track> Tracks
		{
			get;
			set;
		} = new List<Track>();

		/// <summary>
		/// The spotify playback state
		/// </summary>
		public WebPlaybackState WebPlaybackState
		{
			get;
			set;
		}

		/// <summary>
		/// The track that is currently being played on spotify
		/// </summary>
		public Track CurrentlyPlayingTrack
		{
			get;
			set;
		}

		/// <summary>
		/// Component Initialized
		/// </summary>
		protected override async Task OnInitializedAsync()
		{
			try
			{
				var cursorPage = await SpotifyService.GetArtists(AuthenticationService.AuthenticationToken);
				Artists.AddRange(cursorPage.Items);
				
				while (cursorPage != null && cursorPage.Next != null)
				{
					cursorPage = await SpotifyService.GetArtists(AuthenticationService.AuthenticationToken, cursorPage);
					Artists.AddRange(cursorPage.Items);

					StateHasChanged();
				}

				await BindPlaylist();
			}
			catch (Exception ex)
			{
				PageException = ex; 
			}

			Loaded = true;
			
			StateHasChanged();
		}

		/// <summary>
		/// Bind the new playlist to the page
		/// </summary>
		/// <returns></returns>
		public async Task BindPlaylist()
		{
			ShowPlaylistSaved = false;
			Loaded = false;

			Tracks.Clear();

			foreach (var artist in Artists)
			{
				if (Tracks.Count % 45 == 0)
					StateHasChanged();

				Tracks.AddRange((await SpotifyService.GetArtistTopTracks(artist.Id, AuthenticationService.AuthenticationToken)).Take(3).ToList());
			}

			Tracks = Tracks.OrderBy((Track o) => Guid.NewGuid()).ToList();
		}

		/// <summary>
		/// Save the new playlist to the users spotify account
		/// </summary>
		/// <returns></returns>
		public async Task SavePlaylist()
		{
			var playlist = await SpotifyService.CreatePlaylist(AuthenticationService.User, AuthenticationService.AuthenticationToken, "Favorite Artists + Top Songs " + DateTime.Now.ToString(), "Created with Randify!", isPublic: true);

			try
			{
				var tracks = new List<Track>();

				for (int i = 0; i < Tracks.Count; i++)
				{
					if (!string.IsNullOrWhiteSpace(Tracks[i].Id))
					{
						tracks.Add(Tracks[i]);
					}

					if (i % 100 == 0)
					{
						await SpotifyService.AddTracksToPlaylist(AuthenticationService.User, AuthenticationService.AuthenticationToken, playlist, tracks);
						tracks.Clear();
					}
				}

				await SpotifyService.AddTracksToPlaylist(AuthenticationService.User, AuthenticationService.AuthenticationToken, playlist, tracks);
			}
			catch (Exception ex)
			{
				PageException = ex;
			}

			ShowPlaylistSaved = true;

			StateHasChanged();
		}
	}
}
