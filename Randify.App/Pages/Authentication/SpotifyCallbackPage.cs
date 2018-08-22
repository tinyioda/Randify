using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.Extensions.Logging;
using Randify.App.Models;
using Randify.App.Services;
using Randify.App.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Randify.App.Pages.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public class SpotifyCallbackPage : BasePage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitAsync()
        {            
            try
            {
                var uri = UriHelper.GetAbsoluteUri();

                if (uri.Contains("access_denied"))
                {
                    UriHelper.NavigateTo(ConfigurationService.SpotifyLoginUrl);
                }

                var keyValuePairs = uri.Split('#')[1].Split('&');

                var token = new AuthenticationToken();
                token.AccessToken = keyValuePairs.FirstOrDefault(o => o.Contains("access_token")).Split('=')[1];
                token.ExpiresOn = DateTime.Now.AddSeconds(Convert.ToInt32(keyValuePairs.FirstOrDefault(o => o.Contains("expires_in")).Split('=')[1]));
                token.TokenType = keyValuePairs.FirstOrDefault(o => o.Contains("token_type")).Split('=')[1];

                Logger.LogInformation("Access Granted");

                var user = await SpotifyService.GetCurrentUserProfile(token);
                if (user == null)
                {
                    UriHelper.NavigateTo(ConfigurationService.SpotifyLoginUrl);
                }

                AuthenticationService.User = user;
                AuthenticationService.Token = token;

                UriHelper.NavigateTo("Authenticated/Randifier");
            }
            catch (Exception ex)
            {
                PageException = ex;
                Logger.LogError(ex, ex.Message);
            }
        }
    }
}
