using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyWebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Context
    {
        /// <summary>
        /// 
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HREF { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ExternalUrl ExternalUrls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
    }
}
