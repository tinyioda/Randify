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
        private IUriHelper UriHelper { get; set; }

        /// <summary>
        ///
        /// </summary>
        public void Logout()
        {
            AuthenticationService.Current.Logout();

            UriHelper.NavigateTo("/index");
        }
    }
}
