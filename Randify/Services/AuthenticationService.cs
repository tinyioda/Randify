using Blazor.Extensions;
using Microsoft.AspNetCore.Blazor.Browser.Interop;
using Microsoft.AspNetCore.Blazor.Components;
using Randify.Models;
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
        public LocalStorage LocalStorage { get; set; } = new LocalStorage();

        /// <summary>
        /// 
        /// </summary>
        public AuthenticationService()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return User != null && Token != null && !Token.HasExpired;
            }
        } 

        /// <summary>
        /// 
        /// </summary>
        public User User
        {
            get
            {
                return LocalStorage.GetItem<User>("user");
            }
            set
            {
                LocalStorage.SetItem<User>("user", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public AuthenticationToken Token
        {
            get
            {
                return LocalStorage.GetItem<AuthenticationToken>("token");
            }
            set
            {
                LocalStorage.SetItem<AuthenticationToken>("token", value);
            }
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
