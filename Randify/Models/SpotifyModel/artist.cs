using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Randify.Models.SpotifyModel
{
    [JsonObject]
    internal class artist
    {
        public external_urls external_urls { get; set; }

        public followers followers { get; set; }

        public string[] genres { get; set; }

        public string href { get; set; }

        public string id { get; set; }

        public image[] images { get; set; }

        public string name { get; set; }

        public int popularity { get; set; }

        public string type { get; set; }

        public string uri { get; set; }

        public Artist ToPOCO()
        {
            Artist artist = new Artist();
            if (this.external_urls != null)
                artist.ExternalUrl = external_urls.ToPOCO();
            if (this.followers != null)
                artist.Followers = followers.ToPOCO();
            if (this.genres != null)
                artist.Genres = this.genres.ToList();
            if (this.id != null)
               artist.HREF = this.href;
            if (this.id != null)
                artist.Id = this.id;
            if (this.images != null)
            {
                foreach (var image in this.images)
                {
                    var poco = image.ToPOCO();
                    if (poco != null)
                        artist.Images.Add(poco);
                }
            }
            if (this.name != null)
                artist.Name = this.name;
            artist.Popularity = this.popularity;
            if (this.type != null)
                artist.Type = this.type;
            if (this.uri != null)
                artist.Uri = this.uri;

            return artist;
        }
    }
}
