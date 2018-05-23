using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyWebApi.SpotifyModel
{
    [JsonObject]
    internal class error
    {
        public string message { get; set; }
        
        public int status { get; set; }
    }
}
