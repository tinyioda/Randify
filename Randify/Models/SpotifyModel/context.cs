namespace Randify.Models.SpotifyModel
{
	public class context
	{
		public string uri
		{
			get;
			set;
		}

		public Context ToPOCO()
		{
			Context context = new Context();
			context.Uri = uri;
			return context;
		}
	}
}
