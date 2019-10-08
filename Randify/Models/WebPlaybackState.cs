using Microsoft.JSInterop;
using Randify.Models.SpotifyModel;

namespace Randify.Models
{
	public class WebPlaybackState
	{
		public Context Context
		{
			get;
			set;
		}

		public bool Paused
		{
			get;
			set;
		}

		public int Position
		{
			get;
			set;
		}

		public int RepeatMode
		{
			get;
			set;
		}

		public bool Shuffle
		{
			get;
			set;
		}

		public static WebPlaybackState ToPOCOFromJSON(string json)
		{
			return System.Text.Json.JsonSerializer.Deserialize<webplaybackstate>(json).ToPOCO();
		}
	}
}
