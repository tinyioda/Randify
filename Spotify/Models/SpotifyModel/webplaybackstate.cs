using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Models.SpotifyModel
{
    /// <summary>
    /// 
    /// </summary>
    public class webplaybackstate
    {
        public context context { get; set; }
        public bool paused { get; set; }
        public int position { get; set; }
        public int repeat_mode { get; set; }
        public bool shuffle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public WebPlaybackState ToPOCO()
        {
            var webPlaybackState = new WebPlaybackState();
            webPlaybackState.Context = this.context.ToPOCO();
            webPlaybackState.Paused = this.paused;
            webPlaybackState.Position = this.position;
            webPlaybackState.RepeatMode = this.repeat_mode;
            webPlaybackState.Shuffle = this.shuffle;

            return webPlaybackState;
        }
    }
}
