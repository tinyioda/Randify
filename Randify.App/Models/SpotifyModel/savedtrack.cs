using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Randify.Models.SpotifyModel
{
    [JsonObject]
    class savedtrack
    {
        public string added_at { get; set; }
        public track track { get; set; }
    }
}
