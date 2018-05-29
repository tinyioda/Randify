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
        private readonly LocalStorage _localStorage;

        /// <summary>
        /// 
        /// </summary>
        public AuthenticationService(LocalStorage localStorage)
        {
            _localStorage = localStorage;
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
                return _localStorage.GetItem<User>("user");
            }
            set
            {
                _localStorage.SetItem<User>("user", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public AuthenticationToken Token
        {
            get
            {
                return _localStorage.GetItem<AuthenticationToken>("token");
            }
            set
            {
                _localStorage.SetItem<AuthenticationToken>("token", value);
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
