using Spotify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Pages.Authenticated
{
    public class MyArtistsTopSongsPage : Shared.BasePageAuthenticated
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
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Track> Tracks
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public WebPlaybackState WebPlaybackState
        {
            get;
            set;
        }

        /// <summary>
        /// The currently playing track
        /// </summary>
        public Track CurrentlyPlayingTrack
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
                Artists = new List<Artist>();
                Tracks = new List<Track>();

                var page = await SpotifyService.GetArtists(AuthenticationService.AuthenticationToken, null);

                Artists.AddRange(page.Items);

                while (page != null && page.Next != null)
                {
                    page = await SpotifyService.GetArtists(AuthenticationService.AuthenticationToken, page);
                    Artists.AddRange(page.Items);

                    StateHasChanged();
                }

                await BindPlaylist();
            }
            catch (Exception ex)
            {

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
            StateHasChanged();
            
            foreach (var artist in Artists)
            {
                var tracks = await SpotifyService.GetArtistTopTracks(artist.Id, AuthenticationService.AuthenticationToken);
                tracks = tracks.Take(3).ToList();

                Tracks.AddRange(tracks);
                StateHasChanged();
            }
            
            Tracks = Tracks
                .OrderBy(o => Guid.NewGuid())
                .ToList();

            Loaded = true;
            StateHasChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task SavePlaylist()
        {
            var playlist = await SpotifyService.CreatePlaylist(AuthenticationService.User, 
                AuthenticationService.AuthenticationToken, 
                "Favorite Artists + Top Songs " + DateTime.Now.ToString(), 
                "Created with Randify!",
                true);

            try
            {
                var tracks = new List<Track>();

                for (int i = 0; i < Tracks.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Tracks[i].Id) && !string.IsNullOrWhiteSpace(Tracks[i].Id))
                        tracks.Add(Tracks[i]);

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
