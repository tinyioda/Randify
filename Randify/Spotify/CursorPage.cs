using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyWebApi
{
    /// <summary>
    /// The cursor-based paging object is a container for a set of objects. It contains a key called items 
    /// (whose value is an array of the requested objects) along with other keys like next and cursors that 
    /// can be useful in future calls.
    /// </summary>
    public class CursorPage<T> : BaseModel
    {
        /// <summary>
        /// A link to the Web API endpoint returning the full result of the request.
        /// </summary>
        public string HREF { get; set; }

        /// <summary>
        /// The requested data.
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
        /// The cursors used to find the next set of items.
        /// </summary>
        public Cursor Cursors { get; set; }

        /// <summary>
        /// The total number of items available to return.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CursorPage()
        {
            HREF = string.Empty;
            Items = new List<T>();
            Limit = 20;
            Next = string.Empty;
            Cursors = null;
            Total = 0;
        }
    }
}
