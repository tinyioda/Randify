using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Models
{
    /// <summary>
    /// Spotify image
    /// </summary>
    public class Image
    {
        /// <summary>
        /// The image height in pixels
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The source URL of the image.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The image height in pixels
        /// </summary>
        public int Width { get; set; }
    }
}
