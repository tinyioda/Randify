using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyWebApi.SpotifyModel
{
    [JsonObject]
    internal class savedalbum
    {
        public string added_at { get; set; }

        public album album { get; set; }
    }
}
