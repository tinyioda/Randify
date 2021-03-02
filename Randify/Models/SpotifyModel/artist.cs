using System.Linq;

namespace Randify.Models.SpotifyModel
{
	internal class artist
	{
		public external_urls external_urls
		{
			get;
			set;
		}

		public followers followers
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

		public Artist ToPOCO()
		{
			Artist artist = new Artist();
			if (name != null)
			{
				artist.Name = name;
			}
			if (external_urls != null)
			{
				artist.ExternalUrl = external_urls.ToPOCO();
			}
			if (followers != null)
			{
				artist.Followers = followers.ToPOCO();
			}
			if (genres != null)
			{
				artist.Genres = genres.ToList();
			}
			if (id != null)
			{
				artist.HREF = href;
			}
			if (id != null)
			{
				artist.Id = id;
			}
			if (this.images != null)
			{
				image[] images = this.images;
				foreach (image image in images)
				{
					Image image2 = image.ToPOCO();
					if (image2 != null)
					{
						artist.Images.Add(image2);
					}
				}
			}
			artist.Popularity = popularity;
			if (type != null)
			{
				artist.Type = type;
			}
			if (uri != null)
			{
				artist.Uri = uri;
			}
			return artist;
		}
	}
}
