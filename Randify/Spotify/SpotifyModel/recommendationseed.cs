using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyWebApi.SpotifyModel
{
    [JsonObject]
    internal class recommendationseed
    {
        public int afterFilteringSize { get; set; }

        public int afterRelinkingSize { get; set; }

        public string href { get; set; }

        public string id { get; set; }

        public int initialPoolSize { get; set; }

        public string type { get; set; }
    }
}
