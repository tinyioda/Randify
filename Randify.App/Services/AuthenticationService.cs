using Blazor.Extensions;
using Blazor.Extensions.Storage;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Randify.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Randify.App.Services
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
                var task = _localStorage.GetItem<User>("user");
                task.Wait();
                return task.Result;
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
                var task = _localStorage.GetItem<AuthenticationToken>("token");
                task.Wait();
                return task.Result;
            }
            set
            {
                _localStorage.SetItem<AuthenticationToken>("token", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task Logout()
        {
            try
            {
                User = null;
                Token = null;
                await JSRuntime.Current.InvokeAsync<bool>("deleteAllCookies");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
