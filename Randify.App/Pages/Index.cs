using Microsoft.AspNetCore.Blazor.Components;
using Randify.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Randify.App.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public class IndexPage : Randify.App.Shared.BasePage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitAsync()
        {
            if (AuthenticationService.IsAuthenticated)
                UriHelper.NavigateTo("Authenticated/Randifier");
        }
    }
}
