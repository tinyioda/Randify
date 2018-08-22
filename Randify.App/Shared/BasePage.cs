using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.Extensions.Logging;
using Randify.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Randify.App.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public class BasePage : BlazorComponent
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        public Microsoft.AspNetCore.Blazor.Services.IUriHelper UriHelper { get; set; }

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
        [Inject]
        public ILogger<BasePageAuthenticated> Logger { get; set; }

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
            Logger.LogInformation("Location Changed string: " + e);
        }
    }
}
