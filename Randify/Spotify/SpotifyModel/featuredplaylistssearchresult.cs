using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpotifyWebApi.SpotifyModel
{
    [JsonObject]
    internal class featuredplaylistssearchresult
    {
        public string message { get; set; }

        public page<playlist> playlists { get; set; }
    }
}
