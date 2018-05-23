using Newtonsoft.Json;
using SpotifyWebApi.SpotifyModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Components;

namespace SpotifyWebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Player
    {
        /// <summary>
        /// 
        /// </summary>
        private static SpotifyWebApi.HttpHelper HttpHelper { get; set; } = new HttpHelper();

        /// <summary>
        /// Get information about a user’s available devices.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<List<Device>> GetUsersAvailableDevices(AuthenticationToken token)
        {
            var json = await HttpHelper.Get("https://api.spotify.com/v1/me/player/devices", token, true);
            var obj = JsonConvert.DeserializeObject<Dictionary<string, device[]>>(json);

            var devices = new List<Device>();
            
            foreach(var array in obj.Values)
            {
                foreach (var device in array)
                    devices.Add(device.ToPOCO());
            }

            return devices;
        }

        /// <summary>
        /// Start a new context or resume current playback on the user’s active device.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public static async Task StartResumeUsersPlayback(AuthenticationToken token, Device device = null, List<Track> tracks = null, int offset = -1)
        {
            dynamic newObject = new System.Dynamic.ExpandoObject();

            var postData = new Dictionary<string, string>();
            if (offset != -1)
                newObject.offset = offset;
            
            if (tracks != null)
            {
                if (tracks.Any())
                {
                    newObject.uris = new List<string>();

                    foreach (var track in tracks)
                    {
                        newObject.uris.Add("spotify:track:" + track.Id); 
                    }
                }
            }
            
            var jsonInput = JsonConvert.SerializeObject(newObject);

            var uri = "https://api.spotify.com/v1/me/player/play";

            if (device != null)
                uri += "?device_id=" + device.Id;

            var json = await HttpHelper.Put(uri, token, jsonInput, true);
        }

        /// <summary>
        /// Pause playback on the user’s account.
        /// </summary>
        /// <param name="token">Required. A valid access token from the Spotify Accounts service: see the Web API Authorization Guide for details. 
        /// The access token must have been issued on behalf of a premium user. 
        /// The access token must have the user-modify-playback-state scope authorized in order to control playback.</param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user's currently active device is the target.</param>
        /// <returns></returns>
        public static async Task PauseUsersPlayback(AuthenticationToken token, Device device = null)
        {
            var uri = "https://api.spotify.com/v1/me/player/pause";

            if (device != null)
                uri += "?device_id=" + device.Id;

            var json = await HttpHelper.Put(uri, token, null, true);
        }

        /// <summary>
        /// Toggle shuffle on or off for user’s playback.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="shuffle"></param>
        /// <returns></returns>
        public static async Task ToggleShuffleUsersPlayback(AuthenticationToken token, bool shuffle, Device device = null)
        {
            var uri = "https://api.spotify.com/v1/me/player/shuffle?state=" + shuffle;

            if (device != null)
                uri += "&device_id=" + device.Id;

            var json = await HttpHelper.Put(uri, token, null, true);
        }

        /// <summary>
        /// Get information about the user’s current playback state, including track, track progress, and active device.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<CurrentlyPlayingContext> GetUsersCurrentPlaybackInformation(AuthenticationToken token)
        {
            var json = await HttpHelper.Get("https://api.spotify.com/v1/me/player", token, true);
            var obj = JsonConvert.DeserializeObject<currentlyplayingcontext>(json);

            return obj.ToPOCO();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">Required. A valid access token from the Spotify Accounts service: see the Web API Authorization Guide for details. 
        /// The access token must have been issued on behalf of a premium user. 
        /// The access token must have the user-modify-playback-state scope authorized in order to control playback.</param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user's currently active device is the target.</param>
        /// <returns></returns>
        public static async Task SkipPlaybackToNextTrack(AuthenticationToken token, Device device = null)
        {
            var uri = "https://api.spotify.com/v1/me/player/next";

            if (device != null)
                uri += "?device_id=" + device.Id;

            var json = await HttpHelper.Post(uri, token, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">Required. A valid access token from the Spotify Accounts service: see the Web API Authorization Guide for details. 
        /// The access token must have been issued on behalf of a premium user. 
        /// The access token must have the user-modify-playback-state scope authorized in order to control playback.</param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user's currently active device is the target.</param>
        /// <returns></returns>
        public static async Task SkipPlaybackToPreviousTrack(AuthenticationToken token, Device device = null)
        {
            var uri = "https://api.spotify.com/v1/me/player/previous";

            if (device != null)
                uri += "?device_id=" + device.Id;

            var json = await HttpHelper.Post(uri, token, true);
        }
    }
}
