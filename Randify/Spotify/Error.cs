using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyWebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Error
    {
        /// <summary>
        /// A short description of the cause of the error. 
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// The HTTP status code (also returned in the response header; see Response Status Codes for more information).
        /// </summary>
        public int Status
        {
            get;
            set;
        }
    }
}
