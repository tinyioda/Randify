using Blazor.Extensions;
using Microsoft.AspNetCore.Blazor.Browser.Interop;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AuthenticationService> _logger;

        /// <summary>
        /// 
        /// </summary>
        public AuthenticationService(LocalStorage localStorage, ILogger<AuthenticationService> logger)
        {
            _localStorage = localStorage;
            _logger = logger;
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
            try
            {
                User = null;
                Token = null;
                RegisteredFunction.Invoke<bool>("deleteAllCookies");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
