namespace Randify.Models
{
	public class Copyright
	{
		/// <summary>
		/// The copyright text for this album.
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// The type of copyright: C = the copyright, P = the sound recording (performance) copyright.
		/// </summary>
		public string Type { get; set; }
	}
}
