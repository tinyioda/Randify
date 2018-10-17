using Blazor.Extensions;
using Blazor.Extensions.Storage;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
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
        public AuthenticationService(LocalStorage localStorage, ILogger<AuthenticationService> logger)
        {
            _localStorage = localStorage;
            _logger = logger;
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

                await JSRuntime.Current.InvokeAsync<bool>("RandifyJS.deleteAllCookies");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
