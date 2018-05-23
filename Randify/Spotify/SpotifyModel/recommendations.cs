using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyWebApi.SpotifyModel
{
    [JsonObject]
    internal class recommendations
    {
        public recommendationseed seeds { get; set; }

        public track[] tracks { get; set; }
    }
}
