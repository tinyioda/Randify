using Microsoft.JSInterop;
using Randify.Models.SpotifyModel;

namespace Randify.Models
{
	public class WebPlaybackState
	{
		/// <summary>
		/// The current context being played
		/// </summary>
		public Context Context { get; set; }

		/// <summary>
		/// Is paused or not
		/// </summary>
		public bool Paused { get; set; }

		/// <summary>
		/// Current position in ms
		/// </summary>
		public int Position { get; set; }

		/// <summary>
		/// The repeat mode. No repeat mode is 0, once-repeat is 1 and full repeat is 2.
		/// </summary>
		public int RepeatMode { get; set; }

		/// <summary>
		/// Is shuffled or not
		/// </summary>
		public bool Shuffle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="json"></param>
		/// <returns></returns>
		public static WebPlaybackState ToPOCOFromJSON(string json)
		{
			return System.Text.Json.JsonSerializer.Deserialize<webplaybackstate>(json).ToPOCO();
		}
	}
}
