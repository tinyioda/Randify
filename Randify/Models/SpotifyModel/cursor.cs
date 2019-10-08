namespace Randify.Models.SpotifyModel
{
	internal class cursor
	{
		public string after
		{
			get;
			set;
		}

		public Cursor ToPOCO()
		{
			Cursor cursor = new Cursor();
			cursor.After = after;
			return cursor;
		}
	}
}
