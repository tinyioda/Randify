using Microsoft.AspNetCore.Components;
using Spotify.Services;
using System;

namespace Spotify.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public class BasePageAuthenticated : Microsoft.AspNetCore.Components.ComponentBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        public IUriHelper UriHelper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Inject]
        public SpotifyService SpotifyService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Inject]
        public ConfigurationService ConfigurationService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Inject]
        public AuthenticationService AuthenticationService { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Exception PageException { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInit()
        {
            UriHelper.OnLocationChanged += UriHelper_OnLocationChanged;
            base.OnInit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UriHelper_OnLocationChanged(object sender, string e)
        {
            /*
            if (!AuthenticationService.IsAuthenticated)
                UriHelper.NavigateTo("/");
            else
                UriHelper.NavigateTo("/Authenticated/Randifier");
            */
        }
    }
}
