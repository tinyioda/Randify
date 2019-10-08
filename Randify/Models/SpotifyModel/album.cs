using System;
using System.Linq;

namespace Randify.Models.SpotifyModel
{
	internal class album
	{
		public string album_type
		{
			get;
			set;
		}

		public artist[] artists
		{
			get;
			set;
		}

		public string[] available_markets
		{
			get;
			set;
		}

		public copyright[] copyrights
		{
			get;
			set;
		}

		public external_ids external_ids
		{
			get;
			set;
		}

		public external_urls external_urls
		{
			get;
			set;
		}

		public string[] genres
		{
			get;
			set;
		}

		public string href
		{
			get;
			set;
		}

		public string id
		{
			get;
			set;
		}

		public image[] images
		{
			get;
			set;
		}

		public string label
		{
			get;
			set;
		}

		public string name
		{
			get;
			set;
		}

		public int popularity
		{
			get;
			set;
		}

		public string release_date
		{
			get;
			set;
		}

		public string release_date_precision
		{
			get;
			set;
		}

		public page<track> tracks
		{
			get;
			set;
		}

		public string type
		{
			get;
			set;
		}

		public string uri
		{
			get;
			set;
		}

		public Album ToPOCO()
		{
			Album album = new Album();
			if (name != null)
			{
				album.Name = name;
			}
			if (uri != null)
			{
				album.Uri = uri;
			}
			switch (album_type)
			{
			case "album":
				album.AlbumType = AlbumType.Album;
				break;
			case "compilation":
				album.AlbumType = AlbumType.Compilation;
				break;
			case "single":
				album.AlbumType = AlbumType.Single;
				break;
			}
			if (this.artists != null)
			{
				artist[] artists = this.artists;
				foreach (artist artist in artists)
				{
					album.Artists.Add(artist.ToPOCO());
				}
			}
			if (available_markets != null)
			{
				album.AvailableMarkets = available_markets.ToList();
			}
			if (external_ids != null)
			{
				album.ExternalId = external_ids.ToPOCO();
			}
			if (external_urls != null)
			{
				album.ExternalUrl = external_urls.ToPOCO();
			}
			if (genres != null)
			{
				album.Genres = genres.ToList();
			}
			if (href != null)
			{
				album.HREF = href;
			}
			if (id != null)
			{
				album.Id = id;
			}
			if (this.images != null)
			{
				image[] images = this.images;
				foreach (image image in images)
				{
					Image image2 = image.ToPOCO();
					if (image2 != null)
					{
						album.Images.Add(image2);
					}
				}
			}
			if (label != null)
			{
				album.Label = label;
			}
			album.Popularity = popularity;
			if (release_date != null && DateTime.TryParse(release_date, out DateTime result))
			{
				album.ReleaseDate = result;
			}
			if (release_date_precision != null)
			{
				album.ReleaseDatePrecision = release_date_precision;
			}
			if (tracks != null)
			{
				album.Tracks = tracks.ToPOCO<Track>();
			}
			if (type != null)
			{
				album.Type = type;
			}
			return album;
		}
	}
}
