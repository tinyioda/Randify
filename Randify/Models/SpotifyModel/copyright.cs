using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randify.Models.SpotifyModel
{
    [JsonObject]
    internal class copyright
    {
        public string text { get; set; }

        public string type { get; set; }
    }
}
