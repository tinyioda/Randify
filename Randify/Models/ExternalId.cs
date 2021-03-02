namespace Randify.Models
{
	public class ExternalId
	{
		/// <summary>
		/// The identifier type, for example:
		/// - "isrc" - International Standard Recording Code
		/// - "ean" - International Article Number
		/// - "upc" - Universal Product Code
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// An external identifier for the object.
		/// </summary>
		public string Value { get; set; }
	}
}
