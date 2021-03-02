using Randify.Models;
using System;
using System.Threading.Tasks;

namespace Randify.Services
{
	public class AuthenticationService
	{
		public bool IsAuthenticated
		{
			get;
			set;
		}

		public User User
		{
			get;
			set;
		}

		public AuthenticationToken AuthenticationToken
		{
			get;
			set;
		}

		public void Logout()
		{
			try
			{
				User = null;
				AuthenticationToken = null;
				IsAuthenticated = false;
			}
			catch (Exception)
			{
			}
		}
	}
}
