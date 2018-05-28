using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Randify.Models.SpotifyModel
{
    [JsonObject]
    internal class track
    {
        public album album { get; set; }
        public artist[] artists { get; set; }
        public string[] available_markets { get; set; }
        public int disc_number { get; set; }
        public int duration_ms { get; set; }
        public bool @explicit { get; set; }
        public external_ids external_ids { get; set; }
        public external_urls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string preview_url { get; set; }
        public int track_number { get; set; }
        public string type { get; set; }
        public string uri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Track ToPOCO()
        {
            var track = new Track();
            if (album != null)
                track.Album = this.album.ToPOCO();
            if (artists != null)
            {
                foreach (var artist in this.artists)
                    track.Artists.Add(artist.ToPOCO());
            }
            if (available_markets != null)
                track.AvailableMarkets = this.available_markets.ToList();
            track.DiscNumber = this.disc_number;
            track.Duration = this.duration_ms;
            if (external_ids != null)
                track.ExternalId = this.external_ids.ToPOCO();
            if (external_urls != null)
                track.ExternalUrl = this.external_urls.ToPOCO();
            if (href != null)
                track.HREF = this.href;
            if (id != null)
                track.Id = this.id;
            if (name != null)
                track.Name = this.name;
            track.Popularity = this.popularity;
            if (preview_url != null)
                track.PreviewUrl = this.preview_url;
            track.TrackNumber = this.track_number;
            if (type != null)
                track.Type = this.type;
            if (uri != null)
                track.Uri = this.uri;

            return track;
        }
    }
}
