using System.Runtime.CompilerServices;

namespace Randify.Models.SpotifyModel
{
	internal class external_urls
	{
		public string key
		{
			get;
			set;
		}

		public string value
		{
			get;
			set;
		}

		public ExternalUrl ToPOCO()
		{
			return new ExternalUrl
			{
				Key = key,
				Value = value
			};
		}
	}
}
