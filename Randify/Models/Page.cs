using System.Collections.Generic;

namespace Randify.Models
{
	public class Page<T>
	{
		public string HREF
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

		public int Offset
		{
			get;
			set;
		}

		public string Previous
		{
			get;
			set;
		}

		public int Total
		{
			get;
			set;
		}

		public bool HasNextPage
		{
			get
			{
				if (Next == null)
				{
					return false;
				}
				if (string.IsNullOrEmpty(Next) || string.IsNullOrWhiteSpace(Next))
				{
					return false;
				}
				return true;
			}
		}

		public bool HasPreviousPage
		{
			get
			{
				if (Previous == null)
				{
					return false;
				}
				if (string.IsNullOrEmpty(Previous) || string.IsNullOrWhiteSpace(Previous))
				{
					return false;
				}
				return true;
			}
		}

		public Page()
		{
			HREF = null;
			Items = new List<T>();
			Limit = 20;
			Next = null;
			Offset = 0;
			Previous = null;
			Total = 0;
		}
	}
}
