using Microsoft.AspNetCore.Blazor.Components;
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
        /// 
        /// </summary>
        public List<Playlist> Playlists { get; set; } = new List<Playlist>();

        /// <summary>
        /// 
        /// </summary>
        public List<PlaylistTrack> PlaylistTracks { get; set; } = new List<PlaylistTrack>();

        /// <summary>
        /// 
        /// </summary>
        public Playlist CurrentPlaylist { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool PlaylistLoading { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public bool Loaded { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public int NumberOfLoadedTracks { get; set; } = 0;
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitAsync()
        {
            await BindPlaylists();

            if (Playlists.Count > 0)
                await BindPlaylist(Playlists[0].Id);

            Loaded = true;
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

                    Console.WriteLine(page.Next);

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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task BindPlaylist(string playlistId)
        {
            PlaylistLoading = true;
            NumberOfLoadedTracks = 0;

            try
            {
                CurrentPlaylist = Playlists.FirstOrDefault(o => o.Id == playlistId);

                PlaylistTracks.Clear();

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
            catch (Exception ex)
            {
                PageException = ex;
            }

            NumberOfLoadedTracks = PlaylistTracks.Count();
            PlaylistLoading = false;
        }

        /// <summary>
        /// Randomize the currently playlist, remove all of the tracks from the playlist, add all of the tracks back in a random order
        /// </summary>
        /// <returns></returns>
        public async Task Randify()
        {
            var randomTracks = PlaylistTracks
                .Select(o => o.Track)
                .OrderBy(o => Guid.NewGuid())
                .ToList();

            var tracks = new List<Track>();

            // it looks overcomplicated and you're right, but the spotify endpoint has a limit of 100 songs
            try
            {
                for (int i = 0; i < randomTracks.Count(); i++)
                {
                    tracks.Add(randomTracks[i]);

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
            }

            await BindPlaylist(CurrentPlaylist.Id);
        }
    }
}
