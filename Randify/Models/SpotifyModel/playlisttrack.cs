using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Randify.Models.SpotifyModel
{
    [JsonObject]
    internal class playlisttrack
    {
        public string added_at { get; set; }
        public user added_by { get; set; }
        public track track { get; set; }

        public PlaylistTrack ToPOCO()
        {
            PlaylistTrack pt = new PlaylistTrack();

            DateTime addedAt;

            if (DateTime.TryParse(this.added_at, out addedAt))
                pt.AddedAt = addedAt;
            else
                pt.AddedAt = DateTime.Now;

            if (this.added_by != null)
                pt.AddedBy = this.added_by.ToPOCO();

            pt.Track = this.track.ToPOCO();

            return pt;
        }
    }
}
