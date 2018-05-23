using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyWebApi.SpotifyModel
{
    [JsonObject]
    internal class cursor
    {
        public string after { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Cursor ToPOCO()
        {
            return new Cursor() { After = after };
        }
    }
}
