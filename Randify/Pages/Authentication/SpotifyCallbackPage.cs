using Microsoft.AspNetCore.Blazor.Components;
using Randify.Models;
using Randify.Services;
using Randify.Shared;
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
                    UriHelper.NavigateTo(ConfigurationService.SpotifyLoginUrl);
                }

                var keyValuePairs = uri.Split('#')[1].Split('&');

                var token = new AuthenticationToken();
                token.AccessToken = keyValuePairs.FirstOrDefault(o => o.Contains("access_token")).Split('=')[1];
                token.ExpiresOn = DateTime.Now.AddSeconds(Convert.ToInt32(keyValuePairs.FirstOrDefault(o => o.Contains("expires_in")).Split('=')[1]));
                token.TokenType = keyValuePairs.FirstOrDefault(o => o.Contains("token_type")).Split('=')[1];
                
                Console.WriteLine("Access Granted");

                var user = await SpotifyService.GetCurrentUserProfile(token);

                Console.WriteLine("User: " + user.ToString());
                Console.WriteLine("Token: " + token.ToString());

                AuthenticationService.User = user;
                AuthenticationService.Token = token;

                UriHelper.NavigateTo("Authenticated/Randifier");
            }
            catch (Exception ex)
            {
                PageException = ex;
            }
        }
    }
}
