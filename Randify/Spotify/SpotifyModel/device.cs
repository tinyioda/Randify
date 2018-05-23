using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyWebApi.SpotifyModel
{
    [JsonObject]
    internal class device
    {
        public string id { get; set; }

        public bool is_active { get; set; }

        public bool is_restricted { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public int? volume_percent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Device ToPOCO()
        {
            var device = new Device();
            device.Id = id;
            device.IsActive = is_active;
            device.IsRestricted = is_restricted;
            device.Name = name;
            device.Type = type;
            device.VolumePercent = (volume_percent.HasValue) ? volume_percent.Value : 100;
            return device;
        }
    }
}
