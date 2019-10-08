using System;

namespace Randify.Models
{
	public class PlaylistTrack
	{
		public DateTime AddedAt
		{
			get;
			set;
		}

		public User AddedBy
		{
			get;
			set;
		}

		public Track Track
		{
			get;
			set;
		}
	}
}
