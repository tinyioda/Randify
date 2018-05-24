using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Randify.Models.SpotifyModel
{
    [JsonObject]
    internal class album
    {
        public string album_type { get; set; }

        public artist[] artists { get; set; }

        public string[] available_markets { get; set; }

        public copyright[] copyrights { get; set; }

        public external_ids external_ids { get; set; }

        public external_urls external_urls { get; set; }

        public string[] genres { get; set; }

        public string href { get; set; }

        public string id { get; set; }

        public image[] images { get; set; }

        public string label { get; set; }

        public string name { get; set; }

        public int popularity { get; set; }

        public string release_date { get; set; }

        public string release_date_precision { get; set; }

        public page<track> tracks { get; set; }

        public string type { get; set; }

        public string uri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Album ToPOCO()
        {
            var album = new Album();
            switch (album_type)
            {
                case "album":
                    album.AlbumType = AlbumType.Album;
                    break;
                case "compilation":
                    album.AlbumType = AlbumType.Compilation;
                    break;
                case "single":
                    album.AlbumType = AlbumType.Single;
                    break;
            }
            if (artists != null)
            {
                foreach (var artist in artists)
                    album.Artists.Add(artist.ToPOCO());
            }
            if (available_markets != null)
                album.AvailableMarkets = available_markets.ToList();
            if (external_ids != null)
                album.ExternalId = this.external_ids.ToPOCO();
            if (external_urls != null)
                album.ExternalUrl = this.external_urls.ToPOCO();
            if (genres != null)
                album.Genres = this.genres.ToList();
            if (href != null)
                album.HREF = this.href;
            if (id != null)
                album.Id = this.id;
            if (images != null)
            {
                foreach (var image in this.images)
                {
                    var poco = image.ToPOCO();
                    if (poco != null)
                        album.Images.Add(poco);
                }
            }
            if (label != null)
                album.Label = this.label;
            if (name != null)
                album.Name = this.name;
            album.Popularity = this.popularity;
            if (release_date != null)
            {
                DateTime dt;
                if (DateTime.TryParse(this.release_date, out dt))
                    album.ReleaseDate = dt;
            }
            if (release_date_precision != null)
                album.ReleaseDatePrecision = this.release_date_precision;
            if (tracks != null)
                album.Tracks = this.tracks.ToPOCO<Track>();
            if (type != null)
                album.Type = this.type;
            if (uri != null)
                album.Uri = this.uri;

            return album;
        }
    }
}
