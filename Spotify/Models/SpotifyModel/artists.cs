using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Models.SpotifyModel
{
    internal class artists
    {
        public artist[] items { get; set; }

        public List<Artist> ToPOCO()
        {
            var artists = new List<Artist>();

            if (items != null)
            {
                foreach (var item in items)
                {
                    artists.Add(item.ToPOCO());
                }
            }

            return artists;
        }
    }
}
