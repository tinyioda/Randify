using System;
using System.Collections.Generic;

namespace Randify.Models
{
	public class Album
	{
		public AlbumType AlbumType
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

		public List<Copyright> Copyrights
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

		public string Label
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

		public DateTime ReleaseDate
		{
			get;
			set;
		}

		public string ReleaseDatePrecision
		{
			get;
			set;
		}

		public Page<Track> Tracks
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

		public Album()
		{
			AlbumType = AlbumType.Album;
			Artists = new List<Artist>();
			AvailableMarkets = new List<string>();
			ExternalId = null;
			ExternalUrl = null;
			Genres = new List<string>();
			HREF = null;
			Id = null;
			Images = new List<Image>();
			Name = null;
			Popularity = 0;
			ReleaseDate = DateTime.MinValue;
			ReleaseDatePrecision = null;
			Tracks = null;
			Type = null;
			Uri = null;
		}
	}
}
