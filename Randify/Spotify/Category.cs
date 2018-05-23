using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyWebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Category
    {
        /// <summary>
        /// A link to the Web API endpoint returning full details of the category.
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// The category icon, in various sizes.
        /// </summary>
        public Image[] Icons { get; set; }

        /// <summary>
        /// The Spotify category ID of the category.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the category.
        /// </summary>
        public string Name { get; set; }
    }
}
