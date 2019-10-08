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
		/// 
		/// </summary>
		public bool Loaded
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		public bool ShowPlaylistSaved
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		public List<Artist> Artists
		{
			get;
			set;
		} = new List<Artist>();

		/// <summary>
		/// 
		/// </summary>
		public List<Track> Tracks
		{
			get;
			set;
		} = new List<Track>();

		/// <summary>
		/// 
		/// </summary>
		public WebPlaybackState WebPlaybackState
		{
			get;
			set;
		}

		/// <summary>
		/// 
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
				this.PageException = ex; 
			}

			Loaded = true;
			
			StateHasChanged();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public async Task BindPlaylist()
		{
			ShowPlaylistSaved = false;
			Loaded = false;

			Tracks.Clear();

			var count = 0;

			foreach (var artist in Artists)
			{
				Tracks.AddRange((await SpotifyService.GetArtistTopTracks(artist.Id, AuthenticationService.AuthenticationToken)).Take(3).ToList());

				if (count % 11 == 0)
				{
					StateHasChanged();
				}

				count++;
			}

			Tracks = Tracks.OrderBy((Track o) => Guid.NewGuid()).ToList();
			Loaded = true;

			StateHasChanged();
		}

		/// <summary>
		/// 
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
