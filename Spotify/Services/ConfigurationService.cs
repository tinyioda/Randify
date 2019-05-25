using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigurationService
    {
        /// <summary>
        /// 
        /// </summary>
        public ConfigurationService()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public string RedditUrl { get; set; } = "https://www.reddit.com/r/spotify";

        /// <summary>
        /// 
        /// </summary>
        public string SpotifyClientId { get; set; } = "07a41c900d2b407aa5defbceed492634";

        /// <summary>
        /// 
        /// </summary>
        public string SpotifyClientSecret { get; set; } = "60a1e713b89a4125ab6433ce4c8fd97e";

        /// <summary>
        /// 
        /// </summary>
#if DEBUG
        public string SpotifyLoginUrl { get; set; } = "https://accounts.spotify.com/authorize?response_type=token&client_id=07a41c900d2b407aa5defbceed492634&scope=user-read-email%20user-read-birthdate%20streaming%20playlist-read-private%20playlist-modify-private%20playlist-read-collaborative%20playlist-modify-public%20user-read-private%20user-modify-playback-state%20user-read-currently-playing%20user-read-playback-state%20user-follow-read%20user-library-modify%20user-top-read&redirect_uri=http://localhost:53314/Authenticate/SpotifyCallback";
#else
        public string SpotifyLoginUrl { get; set; } = "https://accounts.spotify.com/authorize?response_type=token&client_id=07a41c900d2b407aa5defbceed492634&scope=user-read-email%20user-read-birthdate%20streaming%20playlist-read-private%20playlist-modify-private%20playlist-read-collaborative%20playlist-modify-public%20user-read-private%20user-modify-playback-state%20user-read-currently-playing%20user-read-playback-state%20user-follow-read%20user-library-modify%20user-top-read&redirect_uri=https://randify.azurewebsites.net/Authenticate/SpotifyCallback";
#endif        
    }
}
