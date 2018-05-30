using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.Extensions.Logging;
using Randify.Models;
using Randify.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Randify.Pages.Authenticated
{
    /// <summary>
    /// 
    /// </summary>
    public class RandifierPage : Randify.Shared.BasePageAuthenticated
    {
        /// <summary>
        /// A list of playlists loaded from spotify
        /// </summary>
        public List<Playlist> Playlists { get; set; } = new List<Playlist>();

        /// <summary>
        /// A list of tracks for the currently selected playlist
        /// </summary>
        public List<PlaylistTrack> PlaylistTracks { get; set; } = new List<PlaylistTrack>();

        /// <summary>
        /// The currently selected playlist
        /// </summary>
        public Playlist CurrentPlaylist { get; set; }

        /// <summary>
        /// The currently playing track
        /// </summary>
        public Track CurrentlyPlayingTrack { get; set; }

        /// <summary>
        /// Used to determine if the browser should be rendinering the 'playlist is loading' ui
        /// </summary>
        public bool PlaylistLoading { get; set; } = true;

        /// <summary>
        /// Used to determine if the page has been loaded
        /// </summary>
        public bool Loaded { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public int NumberOfLoadedTracks { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public WebPlaybackState WebPlaybackState { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitAsync()
        {
            await BindPlaylists();

            if (Playlists.Count > 0)
                await BindPlaylist(Playlists[0].Id);

            SpotifyService.EnableSpotifyPlayer(AuthenticationService.Token);
            SpotifyService.SpotifyWebPlayerChanged += SpotifyService_SpotifyWebPlayerChanged;

            Loaded = true;

            base.OnInit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task BindPlaylists()
        {
            try
            {
                var page = await SpotifyService.GetPlaylists(AuthenticationService.User, AuthenticationService.Token);

                do
                {
                    foreach (var playlist in page.Items)
                    {
                        Playlists.Add(playlist);
                    }
                    
                    if (page.HasNextPage)
                        page = await SpotifyService.GetNextPage(page, AuthenticationService.Token);
                    else
                        page = null;
                }
                while (page != null);
            }
            catch (Exception ex)
            {
                PageException = ex;
            }

            StateHasChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task BindPlaylist(string playlistId, List<PlaylistTrack> playlistTracks = null)
        {
            PlaylistLoading = true;
            NumberOfLoadedTracks = 0;

            try
            {
                PlaylistTracks.Clear();

                if (playlistTracks == null)
                {
                    CurrentPlaylist = Playlists.FirstOrDefault(o => o.Id == playlistId);
                    var page = await SpotifyService.GetPlaylistTracks(AuthenticationService.User, AuthenticationService.Token, CurrentPlaylist);
                    do
                    {
                        foreach (var playlistTrack in page.Items)
                        {
                            PlaylistTracks.Add(playlistTrack);
                        }

                        NumberOfLoadedTracks = PlaylistTracks.Count();

                        if (page.HasNextPage)
                            page = await SpotifyService.GetNextPage(page, AuthenticationService.Token);
                        else
                            page = null;
                    }
                    while (page != null);
                }
                else
                    PlaylistTracks.AddRange(playlistTracks);
            }
            catch (Exception ex)
            {
                PageException = ex;
            }

            NumberOfLoadedTracks = PlaylistTracks.Count();
            PlaylistLoading = false;

            StateHasChanged();
        }

        /// <summary>
        /// Randomize the currently playlist, remove all of the tracks from the playlist, add all of the tracks back in a random order
        /// </summary>
        /// <returns></returns>
        public async Task Randify()
        {
            var randomTracks = PlaylistTracks
                .OrderBy(o => Guid.NewGuid())
                .ToList();

            var tracks = new List<Track>();
            
            // it looks overcomplicated and you're right, but the spotify endpoint has a limit of 100 songs
            try
            {
                for (int i = 0; i < PlaylistTracks.Count; i++)
                {
                    tracks.Add(PlaylistTracks[i].Track);

                    if (i % 100 == 0)
                    {
                        await SpotifyService.RemoveTracksFromPlaylist(AuthenticationService.User, AuthenticationService.Token, CurrentPlaylist, tracks);
                        tracks.Clear();
                    }
                }

                await SpotifyService.RemoveTracksFromPlaylist(AuthenticationService.User, AuthenticationService.Token, CurrentPlaylist, tracks);
            }
            catch (Exception ex)
            {
                PageException = ex;
                Logger.LogError(ex, ex.Message);
            }

            tracks.Clear();
            
            // it looks overcomplicated and you're right, but the spotify endpoint has a limit of 100 songs
            try
            {
                for (int i = 0; i < randomTracks.Count; i++)
                {
                    tracks.Add(randomTracks[i].Track);

                    if (i % 100 == 0)
                    {
                        await SpotifyService.AddTracksToPlaylist(AuthenticationService.User, AuthenticationService.Token, CurrentPlaylist, tracks);
                        tracks.Clear();
                    }
                }

                await SpotifyService.AddTracksToPlaylist(AuthenticationService.User, AuthenticationService.Token, CurrentPlaylist, tracks);
            }
            catch (Exception ex)
            {
                PageException = ex;
                Logger.LogError(ex, ex.Message);
            }

            await BindPlaylist(CurrentPlaylist.Id, randomTracks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public void Play(Track track)
        {
            try
            {
                if (WebPlaybackState != null && WebPlaybackState.Paused && CurrentlyPlayingTrack.Id == track.Id)
                {
                    SpotifyService.TogglePlay();
                }
                else
                {
                    CurrentlyPlayingTrack = track;
                    SpotifyService.Play(track.Uri);
                }
            }
            catch (Exception ex)
            {
                PageException = ex;
                Logger.LogError(ex, ex.Message);
            }

            StateHasChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        public void TogglePlay()
        {
            try
            {
                SpotifyService.TogglePlay();
            }
            catch (Exception ex)
            {
                PageException = ex;
                Logger.LogError(ex, ex.Message);
            }

            StateHasChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private void SpotifyService_SpotifyWebPlayerChanged(WebPlaybackState state)
        {
            WebPlaybackState = state;

            StateHasChanged();
        }
    }
}
