using System;

namespace Randify.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class AuthenticationToken
	{
		/// <summary>
		/// An access token that can be provided in subsequent calls, for example to Spotify Web API services. 
		/// 
		/// refreshes the token automatically if it has expired
		/// </summary>
		public string AccessToken { get; set; }

		/// <summary>
		/// How the access token may be used: always "Bearer". 
		/// </summary>
		public string TokenType { get; set; }

		/// <summary>
		/// The date/time that this token will become invalid
		/// </summary>
		public DateTime ExpiresOn { get; set; }

		/// <summary>
		/// A token that can be sent to the Spotify Accounts service in place of an authorization code. 
		/// (When the access code expires, send a POST request to the Accounts service /api/token endpoint, but 
		/// use this code in place of an authorization code. A new access token and a new refresh token will be returned.) 
		/// </summary>
		public string RefreshToken { get; set; }

		/// <summary>
		/// Optional, 
		/// but strongly recommended.The state can be useful for correlating requests and responses.Because your redirect_uri 
		/// can be guessed, using a state value can increase your assurance that an incoming connection is the result of an 
		/// authentication request.If you generate a random string or encode the hash of some client state (e.g., a cookie) 
		/// in this state variable, you can validate the response to additionally ensure that the request and response 
		/// originated in the same browser.This provides protection against attacks such as cross-site request forgery.
		/// See RFC-6749. 
		/// </summary>
		public string State { get; set; }

		/// <summary>
		/// Determines if this token has expired
		/// </summary>
		public bool HasExpired
		{
			get
			{
				return DateTime.Now > ExpiresOn;
			}
		}
	}
}
