using Microsoft.AspNetCore.Blazor.Components;
using Randify.Services;
using Randify.Shared;
using SpotifyWebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Randify.Pages.Authenticate
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
                    Console.WriteLine("Failed to get access token");
                    UriHelper.NavigateTo(ConfigurationService.Current.SpotifyLoginUrl);
                }

                var keyValuePairs = uri.Split('#')[1].Split('&');

                var token = new AuthenticationToken();
                token.AccessToken = keyValuePairs.FirstOrDefault(o => o.Contains("access_token")).Split('=')[1];
                token.ExpiresOn = DateTime.Now.AddSeconds(Convert.ToInt32(keyValuePairs.FirstOrDefault(o => o.Contains("expires_in")).Split('=')[1]));
                token.TokenType = keyValuePairs.FirstOrDefault(o => o.Contains("token_type")).Split('=')[1];
                
                Console.WriteLine("Access Granted");

                var user = await SpotifyWebApi.User.GetCurrentUserProfile(token);
                
                AuthenticationService.Current.User = user;
                AuthenticationService.Current.Token = token;

                UriHelper.NavigateTo("Authenticated/Randifier");
            }
            catch (Exception ex)
            {
                PageException = ex;
            }
        }
    }
}
