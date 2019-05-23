using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Layouts;
using Spotify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public class MainLayoutComponent : LayoutComponentBase
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
        public ConfigurationService ConfigurationService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Inject]
        public AuthenticationService AuthenticationService { get; set; }

        /// <summary>
        ///
        /// </summary>
        public async Task Logout()
        {
            await AuthenticationService.Logout();

            UriHelper.NavigateTo("/");
        }
    }
}
