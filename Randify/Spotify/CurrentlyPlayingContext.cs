using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyWebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class CurrentlyPlayingContext
    {
        /// <summary>
        /// The device that is currently active
        /// </summary>
        public Device Device { get; set; }

        /// <summary>
        /// off, track, context
        /// </summary>
        public string RepeatState { get; set; }

        /// <summary>
        /// If shuffle is on or off
        /// </summary>
        public bool ShuffleState { get; set; }

        /// <summary>
        /// A Context Object. Can be null.
        /// </summary>
        public Context Context { get; set; }

        /// <summary>
        /// Timestamp when data was fetched
        /// </summary>
        public TimeSpan Timestamp { get; set; }

        /// <summary>
        /// Progress into the currently playing track. Can be null.
        /// </summary>
        public int ProgressMilliseconds { get; set; }

        /// <summary>
        /// If something is currently playing.
        /// </summary>
        public bool IsPlaying { get; set; }

        /// <summary>
        /// The currently playing track. Can be null.
        /// </summary>
        public Track Item { get; set; }
    }
}
