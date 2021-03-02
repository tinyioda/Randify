using System.Collections.Generic;

namespace Randify.Models
{
	public class User
	{
		/// <summary>
		/// The country of the user, as set in the user's account profile. An ISO 3166-1 alpha-2 country code. 
		/// This field is only available when the current user has granted access to the user-read-private scope.
		/// </summary>
		public string Country { get; set; }

		/// <summary>
		/// The name displayed on the user's profile. This field is only available when the 
		/// current user has granted access to the user-read-private scope.
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// The user's email address. This field is only available when the current user has granted access to the user-read-email scope.
		/// </summary>
		public string EmailAddress { get; set; }

		/// <summary>
		/// Known external URLs for this user.
		/// </summary>
		public ExternalUrl ExternalUrl { get; set; }

		/// <summary>
		/// A link to the Web API endpoint for this user.
		/// </summary>
		public string HREF { get; set; }

		/// <summary>
		///  The Spotify user ID for the user.  
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// The user's profile image. This field is only available when the current user has granted access to the user-read-private scope.
		/// </summary>
		public List<Image> Images { get; set; }

		/// <summary>
		/// The user's Spotify subscription level: "premium", "free", etc. This field is only available when 
		/// the current user has granted access to the user-read-private scope.
		/// </summary>
		public string Product { get; set; }

		/// <summary>
		/// The object type: "user"  
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// The Spotify URI for the user.
		/// </summary>
		public string Uri { get; set; }
	}
}
