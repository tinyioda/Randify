using Microsoft.AspNetCore.Blazor.Browser.Interop;
using SpotifyWebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Randify.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationService
    {
        /// <summary>
        /// 
        /// </summary>
        public static AuthenticationService Current { get; private set; } = new AuthenticationService();

        /// <summary>
        /// 
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return User != null && Token != null;
            }
        } 

        /// <summary>
        /// 
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AuthenticationToken Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private AuthenticationService()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void Logout()
        {
            User = null;
            Token = null;

            try
            {
                RegisteredFunction.Invoke<bool>("deleteAllCookies");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
