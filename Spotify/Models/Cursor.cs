using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Models
{
    public class Cursor
    {
        /// <summary>
        /// The cursor to use as key to find the next page of items.
        /// </summary>
        public string After
        {
            get;
            set;
        }
    }
}
