namespace Randify.Models.SpotifyModel
{
	internal class playlisttrack
	{
		public track track
		{
			get;
			set;
		}

		public PlaylistTrack ToPOCO()
		{
			PlaylistTrack playlistTrack = new PlaylistTrack();
			playlistTrack.Track = track.ToPOCO();
			return playlistTrack;
		}
	}
}
