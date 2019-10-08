using Microsoft.AspNetCore.Components;
using Randify.Models;
using Randify.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Randify.Pages.Site
{
	public class Stats : BasePageAuthenticated
	{
		/// <summary>
		/// Internal class to capture the genre data
		/// </summary>
		public class GenreData
		{
			/// <summary>
			/// The genre itself
			/// </summary>
			public string Genre
			{
				get;
				set;
			}

			/// <summary>
			/// The percent of total listening % of this genre
			/// </summary>
			public double Percent
			{
				get;
				set;
			}
		}

		/// <summary>
		/// The amount of items that should be displayed
		/// </summary>
		public int DisplayAmount => 25;

		/// <summary>
		/// Is the page loaded or not
		/// </summary>
		public bool Loaded
		{
			get;
			set;
		}

		/// <summary>
		/// List of artists (30 days)
		/// </summary>
		public List<Artist> ShortTermArtists
		{
			get;
			set;
		} = new List<Artist>();

		/// <summary>
		/// List of artists (6 months)
		/// </summary>
		public List<Artist> MediumTermArtists
		{
			get;
			set;
		} = new List<Artist>();
		
		/// <summary>
		/// List of artists (all time)
		/// </summary>
		public List<Artist> LongTermArtists
		{
			get;
			set;
		} = new List<Artist>();

		/// <summary>
		/// List of tracks (30 days)
		/// </summary>
		public List<Track> ShortTermTracks
		{
			get;
			set;
		} = new List<Track>();

		/// <summary>
		/// List of tracks (6 months)
		/// </summary>
		public List<Track> MediumTermTracks
		{
			get;
			set;
		} = new List<Track>();

		/// <summary>
		/// List of tracks (all time)
		/// </summary>
		public List<Track> LongTermTracks
		{
			get;
			set;
		} = new List<Track>();

		/// <summary>
		/// Grouped genre data
		/// </summary>
		public List<GenreData> Genres
		{
			get;
			set;
		} = new List<GenreData>();

		/// <summary>
		/// Component Initialized
		/// </summary>
		protected override async Task OnInitializedAsync()
		{
			try
			{
				var page = await SpotifyService.GetUsersTopArtists(AuthenticationService.AuthenticationToken, 50, TimeRange.ShortTerm);
				ShortTermArtists.AddRange(page.Items);
				
				StateHasChanged();
				
				page = await SpotifyService.GetUsersTopArtists(AuthenticationService.AuthenticationToken);
				MediumTermArtists.AddRange(page.Items);
				
				StateHasChanged();
				
				page = await SpotifyService.GetUsersTopArtists(AuthenticationService.AuthenticationToken, 50, TimeRange.LongTerm);
				LongTermArtists.AddRange(page.Items);
				
				StateHasChanged();
				
				var page2 = await SpotifyService.GetUsersTopTracks(AuthenticationService.AuthenticationToken, 50, TimeRange.ShortTerm);
				ShortTermTracks.AddRange(page2.Items);
				
				StateHasChanged();
				
				page2 = await SpotifyService.GetUsersTopTracks(AuthenticationService.AuthenticationToken);
				MediumTermTracks.AddRange(page2.Items);
				
				StateHasChanged();
				
				page2 = await SpotifyService.GetUsersTopTracks(AuthenticationService.AuthenticationToken, 50, TimeRange.LongTerm);
				LongTermTracks.AddRange(page2.Items);
				
				StateHasChanged();

				var genres = new List<string>();
				ShortTermArtists.ForEach(r => genres.AddRange(r.Genres));
				MediumTermArtists.ForEach(r => genres.AddRange(r.Genres));
				LongTermArtists.ForEach(r => genres.AddRange(r.Genres));
				ShortTermTracks.ForEach(r => genres.AddRange(r.Album.Genres));
				MediumTermTracks.ForEach(r => genres.AddRange(r.Album.Genres));
				LongTermTracks.ForEach(r => genres.AddRange(r.Album.Genres));

				Genres = genres.GroupBy(o => o)
					.Select(g => new GenreData() { Genre = g.Key, Percent = ((double)g.Count() / (double)genres.Count) })
					.OrderByDescending(o => o.Percent)
					.ToList();
			}
			catch (Exception ex)
			{
				PageException = ex;
			}

			Loaded = true;
			
			StateHasChanged();
		}
	}
}
