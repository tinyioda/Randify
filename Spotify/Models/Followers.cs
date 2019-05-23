using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Models
{
    public class Followers
    {
        /// <summary>
        /// A link to the Web API endpoint providing full details of the followers; null if not available.
        /// </summary>
        public string HREF { get; set; }

        /// <summary>
        /// The total number of followers.
        /// </summary>
        public int Total { get; set; }
    }
}
