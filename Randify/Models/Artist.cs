using System.Collections.Generic;

namespace Randify.Models
{
	public class Artist
	{
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

		public List<string> Genres
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
		}

		public string Name
		{
			get;
			set;
		}

		public int Popularity
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

		public Artist()
		{
			ExternalUrl = null;
			Genres = new List<string>();
			HREF = null;
			Id = null;
			Images = new List<Image>();
			Name = null;
			Popularity = 0;
			Type = null;
			Uri = null;
		}
	}
}
