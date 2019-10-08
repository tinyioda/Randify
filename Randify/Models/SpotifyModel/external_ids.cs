using System.Runtime.CompilerServices;

namespace Randify.Models.SpotifyModel
{
	internal class external_ids
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

		public ExternalId ToPOCO()
		{
			return new ExternalId
			{
				Key = key,
				Value = value
			};
		}
	}
}
