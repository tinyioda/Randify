using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Models.SpotifyModel
{
    internal class cursor
    {
        public string after { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Cursor ToPOCO()
        {
            var cursor = new Cursor();
            cursor.After = this.after;
            return cursor;
        }
    }
}
