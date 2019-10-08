using System.Collections.Generic;

namespace Randify.Models
{
	public class Track
	{
		public Album Album
		{
			get;
			set;
		}

		public List<Artist> Artists
		{
			get;
			set;
		}

		public List<string> AvailableMarkets
		{
			get;
			set;
		}

		public int DiscNumber
		{
			get;
			set;
		}

		public int Duration
		{
			get;
			set;
		}

		public bool Explicit
		{
			get;
			set;
		}

		public ExternalId ExternalId
		{
			get;
			set;
		}

		public ExternalUrl ExternalUrl
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

		public string PreviewUrl
		{
			get;
			set;
		}

		public int TrackNumber
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

		public Track()
		{
			Album = null;
			Artists = new List<Artist>();
			AvailableMarkets = new List<string>();
			DiscNumber = 0;
			Duration = 0;
			Explicit = false;
			ExternalId = null;
			ExternalUrl = null;
			HREF = null;
			Id = null;
			Name = null;
			Popularity = 0;
			PreviewUrl = null;
			TrackNumber = 0;
			Type = null;
			Uri = null;
		}
	}
}
