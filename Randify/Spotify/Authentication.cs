using Microsoft.AspNetCore.Blazor.Components;
using Newtonsoft.Json;
using SpotifyWebApi.SpotifyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyWebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Authentication : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        private static SpotifyWebApi.HttpHelper HttpHelper { get; set; } = new HttpHelper();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
        public async static Task<AuthenticationToken> GetAccessToken(string code, string clientId, string clientSecret, string redirectUri)
        {
            var postData = new Dictionary<string, string>();
            postData.Add("grant_type", "authorization_code");
            postData.Add("code", code);
            postData.Add("redirect_uri", redirectUri);
            postData.Add("client_id", clientId);
            postData.Add("client_secret", clientSecret);

            var json = await HttpHelper.PostUnathenticated("https://accounts.spotify.com/api/token", postData);
            var obj = JsonConvert.DeserializeObject<accesstoken>(json);

            return obj.ToPOCO();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="scope"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        public async static Task<AuthenticationToken> RefreshAccessToken(AuthenticationToken token, string scope, string clientId, string clientSecret)
        {
            var postData = new Dictionary<string, string>();
            postData.Add("access_token", token.AccessToken);
            postData.Add("token_type", "Bearer");
            postData.Add("scope", scope);
            postData.Add("expires_in", "3600");
            postData.Add("grant_type", "refresh_token");
            postData.Add("refresh_token", token.RefreshToken);

            byte[] bytes = Encoding.UTF8.GetBytes(clientId + ":" + clientSecret);
            string base64 = Convert.ToBase64String(bytes);

            var json = await HttpHelper.PostBasicAuthentication("https://accounts.spotify.com/api/token", base64, postData);
            var obj = JsonConvert.DeserializeObject<accesstoken>(json);

            return obj.ToPOCO();
        }
    }
}
