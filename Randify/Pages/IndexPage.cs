using Microsoft.AspNetCore.Blazor.Components;
using Randify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Randify.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public class IndexPage : Randify.Shared.BasePage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override void OnInit()
        {
            if (AuthenticationService.IsAuthenticated)
                UriHelper.NavigateTo("Authenticated/Randifier");
        }
    }
}
