namespace Randify.Models.SpotifyModel
{
	internal class followers
	{
		public string href
		{
			get;
			set;
		}

		public int total
		{
			get;
			set;
		}

		public Followers ToPOCO()
		{
			Followers followers = new Followers();
			followers.HREF = href;
			followers.Total = total;
			return followers;
		}
	}
}
