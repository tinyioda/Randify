using Microsoft.AspNetCore.Components;
using Randify.Services;
using System.Threading.Tasks;

namespace Randify.Shared
{
	public class MainLayoutComponent : LayoutComponentBase
	{
		[Inject]
		public NavigationManager NavigationManager
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

		public async Task Logout()
		{
			AuthenticationService.Logout();
			NavigationManager.NavigateTo("/");
		}
	}
}
