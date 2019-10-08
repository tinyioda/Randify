using System.Collections.Generic;

namespace Randify.Models
{
	public class CursorPage<T>
	{
		public string Href
		{
			get;
			set;
		}

		public List<T> Items
		{
			get;
			set;
		}

		public int Limit
		{
			get;
			set;
		}

		public string Next
		{
			get;
			set;
		}

		public Cursor Cursors
		{
			get;
			set;
		}

		public int Total
		{
			get;
			set;
		}

		public CursorPage()
		{
			Items = new List<T>();
		}
	}
}
