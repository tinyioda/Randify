using System.Collections.Generic;

namespace Randify.Models
{
	public class Playlist
	{
		public bool Collaborative
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public ExternalUrl ExternalUrl
		{
			get;
			set;
		}

		public Followers Followers
		{
			get;
			set;
		}

		public string HREF
		{
			get;
			set;
		}

		public string Id
		{
			get;
			set;
		}

		public List<Image> Images
		{
			get;
			set;
		} = new List<Image>();


		public string Name
		{
			get;
			set;
		}

		public User Owner
		{
			get;
			set;
		}

		public bool Public
		{
			get;
			set;
		}

		public Page<PlaylistTrack> Tracks
		{
			get;
			set;
		}

		public string Type
		{
			get;
			set;
		}

		public string Uri
		{
			get;
			set;
		}
	}
}
