using Microsoft.AspNetCore.Blazor;
using Randify.Models.SpotifyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Randify.Models
{
    public class WebPlaybackState
    {
        /// <summary>
        /// 
        /// </summary>
        public Context Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Paused { get; set; }
        
        /// <summary>
        /// Current position in ms
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// The repeat mode. No repeat mode is 0, once-repeat is 1 and full repeat is 2.
        /// </summary>
        public int RepeatMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Shuffle { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static WebPlaybackState ToPOCOFromJSON(string json)
        {
            var obj = Microsoft.JSInterop.Json.Deserialize<webplaybackstate>(json);
            return obj.ToPOCO();
        }
    }
}
