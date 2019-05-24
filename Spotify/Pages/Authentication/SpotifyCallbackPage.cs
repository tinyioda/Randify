using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Spotify.Models;
using Spotify.Services;
using Spotify.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Pages.Authenticate
{
    /// <summary>
    /// 
    /// </summary>
    public class SpotifyCallbackPage : BasePage
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        protected override void OnAfterRender()
        {
            base.OnAfterRender();
        }

        protected override async Task OnAfterRenderAsync()
        {
            try
            {
                var uri = await JSRuntime.InvokeAsync<string>("returnQueryString");

                if (uri.Contains("access_denied") || !uri.Contains("access_token"))
                {
                    UriHelper.NavigateTo(Urls.Index);
                }

                var keyValuePairs = uri.Split('#')[1].Split('&');

                var token = new AuthenticationToken();
                token.AccessToken = keyValuePairs.FirstOrDefault(o => o.Contains("access_token")).Split('=')[1];
                token.ExpiresOn = DateTime.Now.AddSeconds(Convert.ToInt32(keyValuePairs.FirstOrDefault(o => o.Contains("expires_in")).Split('=')[1]));
                token.TokenType = keyValuePairs.FirstOrDefault(o => o.Contains("token_type")).Split('=')[1];

                var user = await SpotifyService.GetCurrentUserProfile(token);

                if (user == null)
                {
                    UriHelper.NavigateTo(Urls.Index);
                }

                AuthenticationService.User = user;
                AuthenticationService.AuthenticationToken = token;
                AuthenticationService.IsAuthenticated = true;

                UriHelper.NavigateTo(Urls.AuthenticatedPlaylistOptions);
            }
            catch (Exception ex)
            {
                PageException = ex;
            }

            //return base.OnAfterRenderAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitAsync()
        {            

        }
    }
}
