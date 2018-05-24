using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Layouts;
using Microsoft.AspNetCore.Blazor.Services;
using Randify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Randify.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public class MainLayoutComponent : BlazorLayoutComponent
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
        public void Logout()
        {
            AuthenticationService.Logout();

            UriHelper.NavigateTo("/index");
        }
    }
}
