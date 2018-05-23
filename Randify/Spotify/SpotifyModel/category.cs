using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyWebApi.SpotifyModel
{
    [JsonObject]
    internal class category
    {
        public string href { get; set; }

        public image[] icons { get; set; }

        public string id { get; set; }

        public string name { get; set; }
    }
}
