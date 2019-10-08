using System.Collections.Generic;

namespace Randify.Models.SpotifyModel
{
	internal class artists
	{
		public artist[] items
		{
			get;
			set;
		}

		public List<Artist> ToPOCO()
		{
			List<Artist> list = new List<Artist>();
			if (this.items != null)
			{
				artist[] items = this.items;
				foreach (artist artist in items)
				{
					list.Add(artist.ToPOCO());
				}
			}
			return list;
		}
	}
}
