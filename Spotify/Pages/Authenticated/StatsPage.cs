using Spotify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Pages.Authenticated
{
    public class StatsPage : Shared.BasePageAuthenticated
    {
        public int Amount
        {
            get
            {
                return 25;
            }
        }

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
        public List<Artist> ShortTermArtists
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Artist> MediumTermArtists
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Artist> LongTermArtists
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public List<Track> ShortTermTracks
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Track> MediumTermTracks
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Track> LongTermTracks
        {
            get;
            set;
        }

        public List<GenreData> Genres
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitAsync()
        {
            base.OnInit();

            try
            {
                ShortTermArtists = new List<Artist>();
                MediumTermArtists = new List<Artist>();
                LongTermArtists = new List<Artist>();

                ShortTermTracks = new List<Track>();
                MediumTermTracks = new List<Track>();
                LongTermTracks = new List<Track>();

                Genres = new List<GenreData>();

                var pageArtists = await SpotifyService.GetUsersTopArtists(AuthenticationService.AuthenticationToken, 50, TimeRange.ShortTerm);
                ShortTermArtists.AddRange(pageArtists.Items);
                
                StateHasChanged();

                pageArtists = await SpotifyService.GetUsersTopArtists(AuthenticationService.AuthenticationToken, 50, TimeRange.MediumTerm);
                MediumTermArtists.AddRange(pageArtists.Items);

                StateHasChanged();

                pageArtists = await SpotifyService.GetUsersTopArtists(AuthenticationService.AuthenticationToken, 50, TimeRange.LongTerm);
                LongTermArtists.AddRange(pageArtists.Items);

                StateHasChanged();

                var pageTracks = await SpotifyService.GetUsersTopTracks(AuthenticationService.AuthenticationToken, 50, TimeRange.ShortTerm);
                ShortTermTracks.AddRange(pageTracks.Items);

                StateHasChanged();

                pageTracks = await SpotifyService.GetUsersTopTracks(AuthenticationService.AuthenticationToken, 50, TimeRange.MediumTerm);
                MediumTermTracks.AddRange(pageTracks.Items);

                StateHasChanged();

                pageTracks = await SpotifyService.GetUsersTopTracks(AuthenticationService.AuthenticationToken, 50, TimeRange.LongTerm);
                LongTermTracks.AddRange(pageTracks.Items);
                               
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

        public class GenreData
        {
            public string Genre { get; set; }
            public double Percent { get; set; }
        }
    }
}
