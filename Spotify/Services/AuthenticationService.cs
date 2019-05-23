using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Spotify.Models;
using System;
using System.Threading.Tasks;

namespace Spotify.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationService
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsAuthenticated
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public User User
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public AuthenticationToken AuthenticationToken
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public AuthenticationService()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public async Task Logout()
        {
            try
            {
                User = null;
                AuthenticationToken = null;
                IsAuthenticated = false;

                // await JSRuntime.Current.InvokeAsync<bool>("RandifyJS.deleteAllCookies");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
