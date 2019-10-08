using System.Collections.Generic;

namespace Randify.Models
{
	public class CursorPage<T>
	{
		/// <summary>
		/// A link to the Web API endpoint returning the full result of the request.
		/// </summary>
		public string Href { get; set; }

		/// <summary>
		/// objects	The requested data.
		/// </summary>
		public List<T> Items { get; set; }

		/// <summary>
		/// The maximum number of items in the response (as set in the query or by default).
		/// </summary>
		public int Limit { get; set; }

		/// <summary>
		/// URL to the next page of items. ( null if none)
		/// </summary>
		public string Next { get; set; }

		/// <summary>
		/// The cursors used to find the next set of items.
		/// </summary>
		public Cursor Cursors { get; set; }

		/// <summary>
		/// The maximum number of items available to return.
		/// </summary>
		public int Total { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public CursorPage()
		{
			Items = new List<T>();
		}
	}
}
