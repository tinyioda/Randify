using Microsoft.AspNetCore.Components;
using Randify.Services;
using System;

namespace Randify.Shared
{
	public class BasePageAuthenticated : ComponentBase
	{
		[Inject]
		public NavigationManager NavigationManager
		{
			get;
			set;
		}

		[Inject]
		public SpotifyService SpotifyService
		{
			get;
			set;
		}
		
		[Inject]
		public AuthenticationService AuthenticationService
		{
			get;
			set;
		}

		public Exception PageException
		{
			get;
			set;
		}

		public BasePageAuthenticated()
		{

		}

		protected override void OnInitialized()
		{
			base.OnInitialized();
		}
	}
}
