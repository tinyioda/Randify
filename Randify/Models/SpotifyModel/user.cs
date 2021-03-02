namespace Randify.Models.SpotifyModel
{
	internal class user
	{
		public string country
		{
			get;
			set;
		}

		public string display_name
		{
			get;
			set;
		}

		public string email
		{
			get;
			set;
		}

		public external_urls external_urls
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

		public string product
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

		public User ToPOCO()
		{
			User user = new User();
			user.Country = country;
			user.DisplayName = display_name;
			user.EmailAddress = email;
			if (external_urls != null)
			{
				user.ExternalUrl = external_urls.ToPOCO();
			}
			user.HREF = href;
			user.Id = id;
			if (this.images != null)
			{
				image[] images = this.images;
				foreach (image image in images)
				{
					Image image2 = image.ToPOCO();
					if (image2 != null)
					{
						user.Images.Add(image2);
					}
				}
			}
			user.Product = product;
			user.Type = type;
			user.Uri = uri;
			return user;
		}
	}
}
