using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Layouts;
using Microsoft.AspNetCore.Blazor.Services;
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
        public AuthenticationService AuthenticationService { get; set; }

        /// <summary>
        ///
        /// </summary>
        public async Task Logout()
        {
            await AuthenticationService.Logout();

            UriHelper.NavigateTo("/index");
        }
    }
}
