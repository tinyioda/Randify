namespace Randify.Models.SpotifyModel
{
	internal class playlist
	{
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

		public page<playlisttrack> tracks
		{
			get;
			set;
		}

		public Playlist ToPOCO()
		{
			Playlist playlist = new Playlist();
			playlist.Id = id;
			playlist.Name = name;
			if (tracks != null)
			{
				playlist.Tracks = tracks.ToPOCO<PlaylistTrack>();
			}
			return playlist;
		}
	}
}
