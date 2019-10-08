namespace Randify.Models.SpotifyModel
{
	internal class image
	{
		public int? height
		{
			get;
			set;
		}

		public string url
		{
			get;
			set;
		}

		public int? width
		{
			get;
			set;
		}

		public Image ToPOCO()
		{
			if (!height.HasValue)
			{
				return null;
			}
			if (!width.HasValue)
			{
				return null;
			}
			return new Image
			{
				Height = height.Value,
				Url = url,
				Width = width.Value
			};
		}
	}
}
