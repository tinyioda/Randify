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
		public class GenreData
		{
			public string Genre
			{
				get;
				set;
			}

			public double Percent
			{
				get;
				set;
			}
		}

		public int Amount => 25;

		public bool Loaded
		{
			get;
			set;
		}

		public List<Artist> ShortTermArtists
		{
			get;
			set;
		} = new List<Artist>();

		public List<Artist> MediumTermArtists
		{
			get;
			set;
		} = new List<Artist>();

		public List<Artist> LongTermArtists
		{
			get;
			set;
		} = new List<Artist>();

		public List<Track> ShortTermTracks
		{
			get;
			set;
		} = new List<Track>();

		public List<Track> MediumTermTracks
		{
			get;
			set;
		} = new List<Track>();

		/// <summary>
		/// 
		/// </summary>
		public List<Track> LongTermTracks
		{
			get;
			set;
		} = new List<Track>();

		/// <summary>
		/// 
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
				var page = await SpotifyService.GetUsersTopArtists(base.AuthenticationService.AuthenticationToken, 50, TimeRange.ShortTerm);
				ShortTermArtists.AddRange(page.Items);
				
				StateHasChanged();
				
				page = await SpotifyService.GetUsersTopArtists(base.AuthenticationService.AuthenticationToken);
				MediumTermArtists.AddRange(page.Items);
				
				StateHasChanged();
				
				page = await SpotifyService.GetUsersTopArtists(base.AuthenticationService.AuthenticationToken, 50, TimeRange.LongTerm);
				LongTermArtists.AddRange(page.Items);
				
				StateHasChanged();
				
				var page2 = await SpotifyService.GetUsersTopTracks(base.AuthenticationService.AuthenticationToken, 50, TimeRange.ShortTerm);
				ShortTermTracks.AddRange(page2.Items);
				
				StateHasChanged();
				
				page2 = await SpotifyService.GetUsersTopTracks(base.AuthenticationService.AuthenticationToken);
				MediumTermTracks.AddRange(page2.Items);
				
				StateHasChanged();
				
				page2 = await SpotifyService.GetUsersTopTracks(base.AuthenticationService.AuthenticationToken, 50, TimeRange.LongTerm);
				LongTermTracks.AddRange(page2.Items);
				
				StateHasChanged();

				var genres = new List<string>();
				ShortTermArtists.ForEach((r) => genres.AddRange(r.Genres));
				MediumTermArtists.ForEach((r) => genres.AddRange(r.Genres));
				LongTermArtists.ForEach((r) => genres.AddRange(r.Genres));
				ShortTermTracks.ForEach((r) => genres.AddRange(r.Album.Genres));
				MediumTermTracks.ForEach((r) => genres.AddRange(r.Album.Genres));
				LongTermTracks.ForEach((r) => genres.AddRange(r.Album.Genres));

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
