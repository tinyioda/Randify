using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Randify.Models;
using Randify.Services;
using Randify.Shared;
using Randify.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Randify.Pages.Auth
{	
	/// <summary>
	/// 
	/// </summary>
	[Route("/Auth/SpotifyCallback")]
	public class SpotifyCallback : BasePage
	{
		/// <summary>
		/// Component Initialized
		/// </summary>
		protected override async Task OnInitializedAsync()
		{
			try
			{
				if (NavigationManager.Uri.Contains("access_denied") || !NavigationManager.Uri.Contains("access_token"))
				{
					NavigationManager.NavigateTo(Urls.Index);
				}

				var keyValuePairs = NavigationManager.Uri.Split('#')[1].Split('&');

				var token = new AuthenticationToken();
				token.AccessToken = keyValuePairs.FirstOrDefault(o => o.Contains("access_token")).Split('=')[1];
				token.ExpiresOn = DateTime.Now.AddSeconds(Convert.ToInt32(keyValuePairs.FirstOrDefault(o => o.Contains("expires_in")).Split('=')[1]));
				token.TokenType = keyValuePairs.FirstOrDefault(o => o.Contains("token_type")).Split('=')[1];

				var user = await SpotifyService.GetCurrentUserProfile(token);

				if (user == null)
				{
					NavigationManager.NavigateTo(Urls.Index);
				}

				AuthenticationService.User = user;
				AuthenticationService.AuthenticationToken = token;
				AuthenticationService.IsAuthenticated = true;

				NavigationManager.NavigateTo(Urls.SiteIndex);
			}
			catch (Exception ex)
			{
				PageException = ex;
			}
		}
	}
}
