using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyWebApi.SpotifyModel
{
    /// <summary>
    /// 
    /// </summary>
    internal class currentlyplayingcontext
    {
        public device device { get; set; }

        public string repeat_state { get; set; }

        public bool shuffle_state { get; set; }

        public context context { get; set; }

        public string timestamp { get; set; }

        public int progress_ms { get; set; }

        public bool is_playing { get; set; }

        public track item { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CurrentlyPlayingContext ToPOCO()
        {
            var currentlyPlayingContext = new CurrentlyPlayingContext();
            currentlyPlayingContext.Device = (this.device == null) ? null : this.device.ToPOCO();
            currentlyPlayingContext.RepeatState = this.repeat_state;
            currentlyPlayingContext.ShuffleState = this.shuffle_state;
            currentlyPlayingContext.Context = (this.context == null) ? null : this.context.ToPOCO();
            currentlyPlayingContext.Timestamp = (this.timestamp == null) ? new TimeSpan(0) : new TimeSpan(0, 0, 0, 0, (int)Int64.Parse(this.timestamp));
            currentlyPlayingContext.ProgressMilliseconds = this.progress_ms;
            currentlyPlayingContext.IsPlaying = this.is_playing;
            currentlyPlayingContext.Item = (this.item == null) ? null : this.item.ToPOCO();

            return currentlyPlayingContext;
        }
    }
}
