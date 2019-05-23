using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Models.SpotifyModel
{
    public class context
    {
        public string uri { get; set; }
        //public string metadata { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Context ToPOCO()
        {
            var context = new Context();
            context.Uri = uri;

            return context;
        }
    }
}
