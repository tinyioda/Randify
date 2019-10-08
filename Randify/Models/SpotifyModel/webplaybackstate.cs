namespace Randify.Models.SpotifyModel
{
	public class webplaybackstate
	{
		public context context
		{
			get;
			set;
		}

		public bool paused
		{
			get;
			set;
		}

		public int position
		{
			get;
			set;
		}

		public int repeat_mode
		{
			get;
			set;
		}

		public bool shuffle
		{
			get;
			set;
		}

		public WebPlaybackState ToPOCO()
		{
			WebPlaybackState webPlaybackState = new WebPlaybackState();
			webPlaybackState.Context = context.ToPOCO();
			webPlaybackState.Paused = paused;
			webPlaybackState.Position = position;
			webPlaybackState.RepeatMode = repeat_mode;
			webPlaybackState.Shuffle = shuffle;
			return webPlaybackState;
		}
	}
}
