namespace Randify.Models.SpotifyModel
{
	internal class page<T>
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

		public int offset
		{
			get;
			set;
		}

		public string previous
		{
			get;
			set;
		}

		public int total
		{
			get;
			set;
		}

		public Page<OUT> ToPOCO<OUT>()
		{
			object obj = null;
			if (typeof(OUT) == typeof(Track))
			{
				obj = new Page<Track>();
				((Page<Track>)obj).HREF = href;
				if (this.items != null)
				{
					T[] items = this.items;
					foreach (object obj2 in items)
					{
						if (obj2.GetType() == typeof(track))
						{
							((Page<Track>)obj).Items.Add(((track)obj2).ToPOCO());
						}
						else if (obj2.GetType() == typeof(savedtrack))
						{
							((Page<Track>)obj).Items.Add(((savedtrack)obj2).track.ToPOCO());
						}
					}
				}
				((Page<Track>)obj).Limit = limit;
				((Page<Track>)obj).Next = next;
				((Page<Track>)obj).Offset = offset;
				((Page<Track>)obj).Previous = previous;
				((Page<Track>)obj).Total = total;
				return (Page<OUT>)obj;
			}
			if (typeof(OUT) == typeof(Artist))
			{
				obj = new Page<Artist>();
				((Page<Artist>)obj).HREF = href;
				if (this.items != null)
				{
					T[] items2 = this.items;
					foreach (object obj3 in items2)
					{
						if (obj3.GetType() == typeof(artist))
						{
							((Page<Artist>)obj).Items.Add(((artist)obj3).ToPOCO());
						}
					}
				}
				((Page<Artist>)obj).Limit = limit;
				((Page<Artist>)obj).Next = next;
				((Page<Artist>)obj).Offset = offset;
				((Page<Artist>)obj).Previous = previous;
				((Page<Artist>)obj).Total = total;
				return (Page<OUT>)obj;
			}
			if (typeof(OUT) == typeof(Album))
			{
				obj = new Page<Album>();
				((Page<Album>)obj).HREF = href;
				if (this.items != null)
				{
					T[] items3 = this.items;
					foreach (object obj4 in items3)
					{
						if (obj4.GetType() == typeof(album))
						{
							((Page<Album>)obj).Items.Add(((album)obj4).ToPOCO());
						}
					}
				}
				((Page<Album>)obj).Limit = limit;
				((Page<Album>)obj).Next = next;
				((Page<Album>)obj).Offset = offset;
				((Page<Album>)obj).Previous = previous;
				((Page<Album>)obj).Total = total;
				return (Page<OUT>)obj;
			}
			if (typeof(OUT) == typeof(Playlist))
			{
				obj = new Page<Playlist>();
				((Page<Playlist>)obj).HREF = href;
				if (this.items != null)
				{
					T[] items4 = this.items;
					foreach (object obj5 in items4)
					{
						if (obj5.GetType() == typeof(playlist))
						{
							((Page<Playlist>)obj).Items.Add(((playlist)obj5).ToPOCO());
						}
					}
				}
				((Page<Playlist>)obj).Limit = limit;
				((Page<Playlist>)obj).Next = next;
				((Page<Playlist>)obj).Offset = offset;
				((Page<Playlist>)obj).Previous = previous;
				((Page<Playlist>)obj).Total = total;
				return (Page<OUT>)obj;
			}
			if (typeof(OUT) == typeof(PlaylistTrack))
			{
				obj = new Page<PlaylistTrack>();
				((Page<PlaylistTrack>)obj).HREF = href;
				if (this.items != null)
				{
					T[] items5 = this.items;
					foreach (object obj6 in items5)
					{
						if (obj6.GetType() == typeof(playlisttrack))
						{
							((Page<PlaylistTrack>)obj).Items.Add(((playlisttrack)obj6).ToPOCO());
						}
					}
				}
				((Page<PlaylistTrack>)obj).Limit = limit;
				((Page<PlaylistTrack>)obj).Next = next;
				((Page<PlaylistTrack>)obj).Offset = offset;
				((Page<PlaylistTrack>)obj).Previous = previous;
				((Page<PlaylistTrack>)obj).Total = total;
				return (Page<OUT>)obj;
			}
			return null;
		}
	}
}
