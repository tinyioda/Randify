using Microsoft.AspNetCore.Blazor.Components;
using Newtonsoft.Json;
using SpotifyWebApi.SpotifyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyWebApi
{
    /// <summary>
    /// Spotify Paged Result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Page<T> : BaseModel
    {
        /// <summary>
        /// A link to the Web API endpoint returning the full result of the request.
        /// </summary>
        public string HREF { get; set; }

        /// <summary>
        /// The requested data of type T
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// The maximum number of items in the response (as set in the query or by default).
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// URL to the next page of items. (null if none) 
        /// </summary>
        public string Next { get; set; }

        /// <summary>
        /// The offset of the items returned (as set in the query or by default).
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// URL to the previous page of items. (null if none) 
        /// </summary>
        public string Previous { get; set; }

        /// <summary>
        /// The total number of items available to return. 
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// True if this object has another page
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                if (Next == null)
                    return false;

                if (string.IsNullOrEmpty(Next) || string.IsNullOrWhiteSpace(Next))
                    return false;

                return true;
            }
        }

        /// <summary>
        /// True if this object has a previous page
        /// </summary>
        public bool HasPreviousPage
        {
            get
            {
                if (Previous == null)
                    return false;

                if (string.IsNullOrEmpty(Previous) || string.IsNullOrWhiteSpace(Previous))
                    return false;

                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static SpotifyWebApi.HttpHelper HttpHelper { get; set; } = new HttpHelper();

        /// <summary>
        /// default constructor of the page object
        /// </summary>
        public Page()
        {
            this.HREF = null;
            this.Items = new List<T>();
            this.Limit = 20;
            this.Next = null;
            this.Offset = 0;
            this.Previous = null;
            this.Total = 0;
        }

        /// <summary>
        /// If this object has a Next page get it
        /// else
        /// throw new Exception("Next page does not exist.");
        /// </summary>
        /// <returns></returns>
        public async Task<Page<T>> GetNextPage(AuthenticationToken token)
        {
            if (!HasNextPage)
                throw new Exception("Next page does not exist.");

            if (typeof(T) == typeof(Track))
            {
                var obj = await HttpHelper.GetJsonAsync<page<track>>(Next, token);
                return obj.ToPOCO<T>();
            }
            else if (typeof(T) == typeof(Playlist))
            {
                var obj = await HttpHelper.GetJsonAsync<page<playlist>>(Next, token);
                return obj.ToPOCO<T>();
            }
            else if (typeof(T) == typeof(Artist))
            {
                var obj = await HttpHelper.GetJsonAsync<page<artist>>(Next, token);
                return obj.ToPOCO<T>();
            }
            else if (typeof(T) == typeof(Album))
            {
                var obj = await HttpHelper.GetJsonAsync<page<album>>(Next, token);
                return obj.ToPOCO<T>();
            }
            else if (typeof(T) == typeof(PlaylistTrack))
            {
                var obj = await HttpHelper.GetJsonAsync<page<playlisttrack>>(Next, token);
                return obj.ToPOCO<T>();
            }

            return null;
        }

        /// <summary>
        /// If this object has a Previous page get it
        /// else
        /// throw new Exception("Previous page does not exist.");
        /// </summary>
        /// <returns></returns>
        public async Task<Page<T>> GetPreviousPage(AuthenticationToken token)
        {
            if (!HasPreviousPage)
                throw new Exception("Previous page does not exist.");

            if (typeof(T) == typeof(Track))
            {
                var obj = await HttpHelper.GetJsonAsync<page<track>>(Previous, token);
                return obj.ToPOCO<T>();
            }
            else if (typeof(T) == typeof(Playlist))
            {
                var obj = await HttpHelper.GetJsonAsync<page<playlist>>(Previous, token);
                return obj.ToPOCO<T>();
            }
            else if (typeof(T) == typeof(Artist))
            {
                var obj = await HttpHelper.GetJsonAsync<page<artist>>(Previous, token);
                return obj.ToPOCO<T>();
            }
            else if (typeof(T) == typeof(Album))
            {
                var obj = await HttpHelper.GetJsonAsync<page<album>>(Previous, token);
                return obj.ToPOCO<T>();
            }
            else if (typeof(T) == typeof(PlaylistTrack))
            {
                var obj = await HttpHelper.GetJsonAsync<page<playlisttrack>>(Previous, token);
                return obj.ToPOCO<T>();
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<List<T>> ToList(AuthenticationToken token)
        {
            List<T> items = new List<T>();

            Page<T> nextPage = (Page<T>)(object)this;

            if (this.Items.Any())
                items.AddRange(nextPage.Items);

            while (nextPage.HasNextPage)
            {
                nextPage = await nextPage.GetNextPage(token);

                if (nextPage.Items.Any())
                    items.AddRange(nextPage.Items);
            }

            return (List<T>)(object)items;
        }
    }
}
