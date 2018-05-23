using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyWebApi.SpotifyModel
{
    [JsonObject]
    internal class cursorpage<T>
    {
        public string href { get; set; }

        public T[] items { get; set; }

        public int limit { get; set; }

        public string next { get; set; }

        public cursor cursors { get; set; }

        public int total { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="OUT"></typeparam>
        /// <returns></returns>
        public CursorPage<OUT> ToPOCO<OUT>()
        {
            object page = null;
            if (typeof(OUT) == typeof(Track))
            {
                page = new CursorPage<Track>();

                ((CursorPage<Track>)page).HREF = this.href;
                if (items != null)
                {
                    foreach (object item in items)
                    {
                        if (item.GetType() == typeof(track))
                            ((CursorPage<Track>)page).Items.Add(((track)(object)item).ToPOCO());
                        else if (item.GetType() == typeof(savedtrack))
                            ((CursorPage<Track>)page).Items.Add(((savedtrack)(object)item).track.ToPOCO());
                    }
                }
                ((CursorPage<Track>)page).Limit = this.limit;
                ((CursorPage<Track>)page).Next = this.next;
                ((CursorPage<Track>)page).Cursors = this.cursors.ToPOCO();
                ((CursorPage<Track>)page).Total = this.total;

                return ((CursorPage<OUT>)page);
            }

            if (typeof(OUT) == typeof(Artist))
            {
                page = new CursorPage<Artist>();

                ((CursorPage<Artist>)page).HREF = this.href;
                if (items != null)
                {
                    foreach (object item in items)
                    {
                        if (item.GetType() == typeof(Artist))
                            ((CursorPage<Artist>)page).Items.Add(((artist)(object)item).ToPOCO());
                    }
                }
                ((CursorPage<Artist>)page).Limit = this.limit;
                ((CursorPage<Artist>)page).Next = this.next;
                ((CursorPage<Artist>)page).Cursors = this.cursors.ToPOCO();
                ((CursorPage<Artist>)page).Total = this.total;

                return ((CursorPage<OUT>)page);
            }

            if (typeof(OUT) == typeof(Album))
            {
                page = new CursorPage<Album>();

                ((CursorPage<Album>)page).HREF = this.href;
                if (items != null)
                {
                    foreach (object item in items)
                    {
                        if (item.GetType() == typeof(Album))
                            ((CursorPage<Album>)page).Items.Add(((album)(object)item).ToPOCO());
                    }
                }
                ((CursorPage<Album>)page).Limit = this.limit;
                ((CursorPage<Album>)page).Next = this.next;
                ((CursorPage<Album>)page).Cursors = this.cursors.ToPOCO();
                ((CursorPage<Album>)page).Total = this.total;

                return ((CursorPage<OUT>)page);
            }

            if (typeof(OUT) == typeof(Playlist))
            {
                page = new CursorPage<Playlist>();

                ((CursorPage<Playlist>)page).HREF = this.href;
                if (items != null)
                {
                    foreach (object item in items)
                    {
                        if (item.GetType() == typeof(Playlist))
                            ((CursorPage<Playlist>)page).Items.Add(((playlist)(object)item).ToPOCO());
                    }
                }
                ((CursorPage<Playlist>)page).Limit = this.limit;
                ((CursorPage<Playlist>)page).Next = this.next;
                ((CursorPage<Playlist>)page).Cursors = this.cursors.ToPOCO();
                ((CursorPage<Playlist>)page).Total = this.total;

                return ((CursorPage<OUT>)page);
            }

            if (typeof(OUT) == typeof(PlaylistTrack))
            {
                page = new CursorPage<PlaylistTrack>();

                ((CursorPage<PlaylistTrack>)page).HREF = this.href;
                if (items != null)
                {
                    foreach (object item in items)
                    {
                        if (item.GetType() == typeof(PlaylistTrack))
                            ((CursorPage<PlaylistTrack>)page).Items.Add(((playlisttrack)(object)item).ToPOCO());
                    }
                }
                ((CursorPage<PlaylistTrack>)page).Limit = this.limit;
                ((CursorPage<PlaylistTrack>)page).Next = this.next;
                ((CursorPage<PlaylistTrack>)page).Cursors = this.cursors.ToPOCO();
                ((CursorPage<PlaylistTrack>)page).Total = this.total;

                return ((CursorPage<OUT>)page);
            }

            return null;
        }
    }
}
