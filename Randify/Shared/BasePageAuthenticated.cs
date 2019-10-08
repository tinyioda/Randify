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

		protected override void OnInitialized()
		{
			NavigationManager.LocationChanged += NavigationManager_LocationChanged;

			base.OnInitialized();
		}

		private void NavigationManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
		{

		}

		public BasePageAuthenticated()
		{

		}
	}
}
