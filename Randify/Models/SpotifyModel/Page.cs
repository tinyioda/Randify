using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Randify.Models.SpotifyModel
{
    internal class page<T>
    {
        public string href { get; set; }
        public T[] items { get; set; }
        public int limit { get; set; }
        public string next { get; set; }
        public int offset { get; set; }
        public string previous { get; set; }
        public int total { get; set; }

        public Page<OUT> ToPOCO<OUT>()
        {
            object page = null;
            if (typeof(OUT) == typeof(Track))
            {
                page = new Page<Track>();

                ((Page<Track>)page).HREF = this.href;
                if (items != null)
                {
                    foreach (object item in items)
                    {
                        if (item.GetType() == typeof(track))
                            ((Page<Track>)page).Items.Add(((track)(object)item).ToPOCO());
                        else if (item.GetType() == typeof(savedtrack))
                            ((Page<Track>)page).Items.Add(((savedtrack)(object)item).track.ToPOCO());
                    }
                }
                ((Page<Track>)page).Limit = this.limit;
                ((Page<Track>)page).Next = this.next;
                ((Page<Track>)page).Offset = this.offset;
                ((Page<Track>)page).Previous = this.previous;
                ((Page<Track>)page).Total = this.total;

                return ((Page<OUT>)page);
            }

            if (typeof(OUT) == typeof(Artist))
            {
                page = new Page<Artist>();

                ((Page<Artist>)page).HREF = this.href;
                if (items != null)
                {
                    foreach (object item in items)
                    {
                        if (item.GetType() == typeof(artist))
                            ((Page<Artist>)page).Items.Add(((artist)(object)item).ToPOCO());
                    }
                }
                ((Page<Artist>)page).Limit = this.limit;
                ((Page<Artist>)page).Next = this.next;
                ((Page<Artist>)page).Offset = this.offset;
                ((Page<Artist>)page).Previous = this.previous;
                ((Page<Artist>)page).Total = this.total;

                return ((Page<OUT>)page);
            }

            if (typeof(OUT) == typeof(Album))
            {
                page = new Page<Album>();

                ((Page<Album>)page).HREF = this.href;
                if (items != null)
                {
                    foreach (object item in items)
                    {
                        if (item.GetType() == typeof(album))
                            ((Page<Album>)page).Items.Add(((album)(object)item).ToPOCO());
                    }
                }
                ((Page<Album>)page).Limit = this.limit;
                ((Page<Album>)page).Next = this.next;
                ((Page<Album>)page).Offset = this.offset;
                ((Page<Album>)page).Previous = this.previous;
                ((Page<Album>)page).Total = this.total;

                return ((Page<OUT>)page);
            }

            if (typeof(OUT) == typeof(Playlist))
            {
                page = new Page<Playlist>();

                ((Page<Playlist>)page).HREF = this.href;
                if (items != null)
                {
                    foreach (object item in items)
                    {
                        if (item.GetType() == typeof(playlist))
                            ((Page<Playlist>)page).Items.Add(((playlist)(object)item).ToPOCO());
                    }
                }
                ((Page<Playlist>)page).Limit = this.limit;
                ((Page<Playlist>)page).Next = this.next;
                ((Page<Playlist>)page).Offset = this.offset;
                ((Page<Playlist>)page).Previous = this.previous;
                ((Page<Playlist>)page).Total = this.total;

                return ((Page<OUT>)page);
            }

            if (typeof(OUT) == typeof(PlaylistTrack))
            {
                page = new Page<PlaylistTrack>();

                ((Page<PlaylistTrack>)page).HREF = this.href;
                if (items != null)
                {
                    foreach (object item in items)
                    {
                        if (item.GetType() == typeof(playlisttrack))
                            ((Page<PlaylistTrack>)page).Items.Add(((playlisttrack)(object)item).ToPOCO());
                    }
                }
                ((Page<PlaylistTrack>)page).Limit = this.limit;
                ((Page<PlaylistTrack>)page).Next = this.next;
                ((Page<PlaylistTrack>)page).Offset = this.offset;
                ((Page<PlaylistTrack>)page).Previous = this.previous;
                ((Page<PlaylistTrack>)page).Total = this.total;

                return ((Page<OUT>)page);
            }

            return null;
        }
    }
}
