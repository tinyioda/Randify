using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Spotify.Models;
using Spotify.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Spotify.Pages.Authenticated
{
    /// <summary>
    /// 
    /// </summary>
    public class RandifierPage : Spotify.Shared.BasePageAuthenticated
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
        public bool IsPlaylistLoading { get; set; } = false;

        /// <summary>
        /// Used to determine if the application is currently randifying a playlist.
        /// </summary>
        public bool IsRandifying { get; set; } = false;

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
        public Stopwatch Stopwatch { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitAsync()
        {
            await BindPlaylists();
            
            SpotifyService.EnableSpotifyPlayer(AuthenticationService.AuthenticationToken);
            SpotifyService.SpotifyWebPlayerChanged += SpotifyService_SpotifyWebPlayerChanged;

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
                var page = await SpotifyService.GetPlaylists(AuthenticationService.User, AuthenticationService.AuthenticationToken);

                do
                {
                    foreach (var playlist in page.Items)
                    {
                        Playlists.Add(playlist);
                    }
                    
                    if (page.HasNextPage)
                        page = await SpotifyService.GetNextPage(page, AuthenticationService.AuthenticationToken);
                    else
                        page = null;
                }
                while (page != null);
            }
            catch (Exception ex)
            {
                PageException = ex;
            }

            Loaded = true;

            StateHasChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task BindPlaylist(UIChangeEventArgs e)
        {
            if (IsRandifying || IsPlaylistLoading)
                return;

            var playlistId = e.Value.ToString();

            try
            {
                IsPlaylistLoading = true;
                Seconds = 0;
                NumberOfLoadedTracks = 0;

                Stopwatch = new Stopwatch();
                Stopwatch.Start();
                PlaylistTracks.Clear();

                try
                {
                    var tracks = new List<PlaylistTrack>();

                    CurrentPlaylist = Playlists.FirstOrDefault(o => o.Id == playlistId);
                    var page = await SpotifyService.GetPlaylistTracks(AuthenticationService.User, AuthenticationService.AuthenticationToken, CurrentPlaylist);
                    do
                    {
                        foreach (var playlistTrack in page.Items)
                        {
                            tracks.Add(playlistTrack);
                        }

                        Seconds = Stopwatch.Elapsed.Seconds;
                        NumberOfLoadedTracks = tracks.Count();

                        StateHasChanged();

                        if (page.HasNextPage)
                            page = await SpotifyService.GetNextPage(page, AuthenticationService.AuthenticationToken);
                        else
                            page = null;
                    }
                    while (page != null);
                            
                    PlaylistTracks = tracks;
                }
                catch (Exception ex)
                {
                    PageException = ex;
                }

                NumberOfLoadedTracks = PlaylistTracks.Count();
                IsPlaylistLoading = false;
                Stopwatch.Stop();

                StateHasChanged();
            }
            catch (Exception ex)
            {
                PageException = ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task BindPlaylistAfterSort(List<PlaylistTrack> playlistTracks)
        {
            PlaylistTracks.Clear();
            PlaylistTracks.AddRange(playlistTracks);

            NumberOfLoadedTracks = PlaylistTracks.Count();
            IsPlaylistLoading = false;
            Stopwatch.Stop();

            StateHasChanged();
        }

        /// <summary>
        /// Randomize the currently playlist, remove all of the tracks from the playlist, add all of the tracks back in a random order
        /// </summary>
        /// <returns></returns>
        public async Task Randify()
        {
            if (IsRandifying || IsPlaylistLoading)
                return;

            IsRandifying = true;

            var currentPlaylist = CurrentPlaylist;
            var randomTracks = PlaylistTracks.ToList();
            var listOfSkippedTracks = new List<PlaylistTrack>();

            var tracks = new List<Track>();

            // it looks overcomplicated and you're right, but the spotify endpoint has a limit of 100 songs
            try
            {
                for (int i = 0; i < randomTracks.Count; i++)
                {
                    if (!string.IsNullOrEmpty(randomTracks[i].Track.Id) && !string.IsNullOrWhiteSpace(randomTracks[i].Track.Id))
                        tracks.Add(randomTracks[i].Track);
                    else
                        listOfSkippedTracks.Add(randomTracks[i]);

                    if (i % 100 == 0)
                    {
                        await SpotifyService.RemoveTracksFromPlaylist(AuthenticationService.User, AuthenticationService.AuthenticationToken, currentPlaylist, tracks);
                        tracks.Clear();
                    }
                }

                await SpotifyService.RemoveTracksFromPlaylist(AuthenticationService.User, AuthenticationService.AuthenticationToken, currentPlaylist, tracks);
            }
            catch (Exception ex)
            {
                PageException = ex;
            }

            tracks.Clear();

            randomTracks = randomTracks
                .OrderBy(o => Guid.NewGuid())
                .ToList();

            // it looks overcomplicated and you're right, but the spotify endpoint has a limit of 100 songs
            try
            {
                for (int i = 0; i < randomTracks.Count; i++)
                {
                    if (!string.IsNullOrEmpty(randomTracks[i].Track.Id) && !string.IsNullOrWhiteSpace(randomTracks[i].Track.Id))
                        tracks.Add(randomTracks[i].Track);

                    if (i % 100 == 0)
                    {
                        await SpotifyService.AddTracksToPlaylist(AuthenticationService.User, AuthenticationService.AuthenticationToken, currentPlaylist, tracks);
                        tracks.Clear();
                    }
                }

                await SpotifyService.AddTracksToPlaylist(AuthenticationService.User, AuthenticationService.AuthenticationToken, currentPlaylist, tracks);
            }
            catch (Exception ex)
            {
                PageException = ex;
            }
            
            foreach (var skippedTrack in listOfSkippedTracks)
            {
                randomTracks.Remove(skippedTrack);
            }

            listOfSkippedTracks.Reverse();

            foreach (var skippedTrack in listOfSkippedTracks)
            {
                randomTracks.Insert(0, skippedTrack);
            }

            IsRandifying = false;

            await BindPlaylistAfterSort(randomTracks);
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
