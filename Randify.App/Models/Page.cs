using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randify.App.Models
{
    /// <summary>
    /// Spotify Paged Result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Page<T>
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
    }
}
