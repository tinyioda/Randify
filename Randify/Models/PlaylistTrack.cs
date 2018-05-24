using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Randify.Models
{
    public class PlaylistTrack
    {
        /// <summary>
        /// The date and time the track was added
        /// Note that some very old playlists may return DateTime.Min in this field.
        /// </summary>
        public DateTime AddedAt { get; set; }

        /// <summary>
        /// The Spotify user who added the track.
        /// Note that some very old playlists may return null in this field.
        /// </summary>
        public User AddedBy { get; set; }

        /// <summary>
        /// Information about the track.
        /// </summary>
        public Track Track { get; set; }
    }
}
