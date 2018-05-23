using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
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
    public class BasePageAuthenticated : BlazorComponent
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        public Microsoft.AspNetCore.Blazor.Services.IUriHelper UriHelper { get; set; }
        
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
            Console.WriteLine("Location Changed string: " + e);
            Console.WriteLine("Location Changed object: " + sender);

            /*
            if (!AuthenticationService.Current.IsAuthenticated)
                UriHelper.NavigateTo("/index");
            else
                UriHelper.NavigateTo("/Authenticated/Randifier");
            */
        }
    }
}
