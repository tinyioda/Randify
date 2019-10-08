namespace Randify.Models.SpotifyModel
{
	internal class track
	{
		public album album
		{
			get;
			set;
		}

		public artist[] artists
		{
			get;
			set;
		}

		public string id
		{
			get;
			set;
		}

		public string name
		{
			get;
			set;
		}

		public string uri
		{
			get;
			set;
		}

		public Track ToPOCO()
		{
			Track track = new Track();
			if (album != null)
			{
				track.Album = album.ToPOCO();
			}
			if (this.artists != null)
			{
				artist[] artists = this.artists;
				foreach (artist artist in artists)
				{
					track.Artists.Add(artist.ToPOCO());
				}
			}
			if (id != null)
			{
				track.Id = id;
			}
			if (name != null)
			{
				track.Name = name;
			}
			if (uri != null)
			{
				track.Uri = uri;
			}
			return track;
		}
	}
}
