using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Models.SpotifyModel
{
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

            if (typeof(OUT) == typeof(Artist))
            {
                page = new CursorPage<Artist>();

                ((CursorPage<Artist>)page).Href = this.href;
                if (items != null)
                {
                    foreach (object item in items)
                    {
                        if (item.GetType() == typeof(artist))
                            ((CursorPage<Artist>)page).Items.Add(((artist)(object)item).ToPOCO());
                    }
                }
                ((CursorPage<Artist>)page).Limit = this.limit;
                ((CursorPage<Artist>)page).Next = this.next;
                if (this.cursors != null)
                    ((CursorPage<Artist>)page).Cursors = this.cursors.ToPOCO();
                ((CursorPage<Artist>)page).Total = this.total;

                return ((CursorPage<OUT>)page);
            }

            return null;
        }
    }
}
