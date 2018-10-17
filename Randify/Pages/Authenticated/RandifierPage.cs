using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.Extensions.Logging;
using Randify.Models;
using Randify.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

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
        public User User { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AuthenticationToken AuthenticationToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitAsync()
        {
            User = AuthenticationService.User;
            AuthenticationToken = AuthenticationService.AuthenticationToken;

            await BindPlaylists();
            
            SpotifyService.EnableSpotifyPlayer(AuthenticationToken);
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
                var page = await SpotifyService.GetPlaylists(User, AuthenticationToken);

                do
                {
                    foreach (var playlist in page.Items)
                    {
                        Playlists.Add(playlist);
                    }
                    
                    if (page.HasNextPage)
                        page = await SpotifyService.GetNextPage(page, AuthenticationToken);
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
        public async Task BindPlaylist(string playlistId, List<PlaylistTrack> playlistTracks = null)
        {
            if (IsRandifying || IsPlaylistLoading)
                return;

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
                    if (playlistTracks == null)
                    {
                        var tracks = new List<PlaylistTrack>();

                        CurrentPlaylist = Playlists.FirstOrDefault(o => o.Id == playlistId);
                        var page = await SpotifyService.GetPlaylistTracks(User, AuthenticationToken, CurrentPlaylist);
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
                                page = await SpotifyService.GetNextPage(page, AuthenticationToken);
                            else
                                page = null;
                        }
                        while (page != null);
                            
                        PlaylistTracks = tracks;
                    }
                    else
                        PlaylistTracks.AddRange(playlistTracks);
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
                Logger.LogError(ex, ex.Message);
            }
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
                        await SpotifyService.RemoveTracksFromPlaylist(User, AuthenticationToken, currentPlaylist, tracks);
                        tracks.Clear();
                    }
                }

                await SpotifyService.RemoveTracksFromPlaylist(User, AuthenticationToken, currentPlaylist, tracks);
            }
            catch (Exception ex)
            {
                PageException = ex;
                Logger.LogError(ex, ex.Message);
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
                        await SpotifyService.AddTracksToPlaylist(User, AuthenticationToken, currentPlaylist, tracks);
                        tracks.Clear();
                    }
                }

                await SpotifyService.AddTracksToPlaylist(User, AuthenticationToken, currentPlaylist, tracks);
            }
            catch (Exception ex)
            {
                PageException = ex;
                Logger.LogError(ex, ex.Message);
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

            await BindPlaylist(currentPlaylist.Id, randomTracks);
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
