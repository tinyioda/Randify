using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpotifyWebApi.SpotifyModel
{
    [JsonObject]
    class savedtrack
    {
        public string added_at { get; set; }
        public track track { get; set; }
    }
}
