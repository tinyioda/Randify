using System;

namespace Randify.Models
{
	public class AuthenticationToken
	{
		public string AccessToken
		{
			get;
			set;
		}

		public string TokenType
		{
			get;
			set;
		}

		public DateTime ExpiresOn
		{
			get;
			set;
		}

		public string RefreshToken
		{
			get;
			set;
		}

		public string State
		{
			get;
			set;
		}

		public bool HasExpired => DateTime.Now > ExpiresOn;
	}
}
