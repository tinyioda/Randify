namespace Randify.Models.SpotifyModel
{
	internal class cursorpage<T>
	{
		public string href
		{
			get;
			set;
		}

		public T[] items
		{
			get;
			set;
		}

		public int limit
		{
			get;
			set;
		}

		public string next
		{
			get;
			set;
		}

		public cursor cursors
		{
			get;
			set;
		}

		public int total
		{
			get;
			set;
		}

		public CursorPage<OUT> ToPOCO<OUT>()
		{
			object obj = null;
			if (typeof(OUT) == typeof(Artist))
			{
				obj = new CursorPage<Artist>();
				((CursorPage<Artist>)obj).Href = href;
				if (this.items != null)
				{
					T[] items = this.items;
					foreach (object obj2 in items)
					{
						if (obj2.GetType() == typeof(artist))
						{
							((CursorPage<Artist>)obj).Items.Add(((artist)obj2).ToPOCO());
						}
					}
				}
				((CursorPage<Artist>)obj).Limit = limit;
				((CursorPage<Artist>)obj).Next = next;
				if (cursors != null)
				{
					((CursorPage<Artist>)obj).Cursors = cursors.ToPOCO();
				}
				((CursorPage<Artist>)obj).Total = total;
				return (CursorPage<OUT>)obj;
			}
			return null;
		}
	}
}
