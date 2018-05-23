using Microsoft.AspNetCore.Blazor.Browser.Http;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;

namespace SpotifyWebApi
{
    /// <summary>
    /// A helper class used as an interface for common HttpClient commands
    /// </summary>
    internal class HttpHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        public HttpHelper()
        {
            
        }

        /// <summary>
        /// Downloads a url and reads its contents as a string using the get method
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> Get(string url)
        {
            var httpClient = new HttpClient(new BrowserHttpMessageHandler())
            {
                BaseAddress = new Uri(new BrowserUriHelper().GetBaseUriPrefix())
            };

            var httpResponse = await httpClient.GetAsync(url);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Downloads a url and reads its contents as a string, requires an authorization token
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> Get(string url, AuthenticationToken token, bool includeBearer = true)
        {
            var httpClient = new HttpClient(new BrowserHttpMessageHandler())
            {
                BaseAddress = new Uri(new BrowserUriHelper().GetBaseUriPrefix())
            };

            if (includeBearer)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            else
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.AccessToken);

            var httpResponse = await httpClient.GetAsync(url);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Downloads a url and reads its contents as a string, requires an authorization token
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<T> GetJsonAsync<T>(string url, AuthenticationToken token, bool includeBearer = true)
        {
            var httpClient = new HttpClient(new BrowserHttpMessageHandler())
            {
                BaseAddress = new Uri(new BrowserUriHelper().GetBaseUriPrefix())
            };

            if (includeBearer)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            else
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.AccessToken);

            var result = await httpClient.GetJsonAsync<T>(url);

            Console.WriteLine(result);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> PostBasicAuthentication(string url, string token, Dictionary<string, string> postData = null)
        {
            HttpContent content = null;
            if (postData != null)
                content = new FormUrlEncodedContent(postData.ToArray<KeyValuePair<string, string>>());
            else
                content = null;

            var httpClient = new HttpClient(new BrowserHttpMessageHandler())
            {
                BaseAddress = new Uri(new BrowserUriHelper().GetBaseUriPrefix())
            };

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);

            var httpResponse = await httpClient.PostAsync(url, content);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// posts data to a url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public async Task<string> PostUnathenticated(string url, Dictionary<string, string> postData = null)
        {
            HttpContent content = null;
            if (postData != null)
                content = new FormUrlEncodedContent(postData.ToArray<KeyValuePair<string, string>>());
            else
                content = null;

            var httpClient = new HttpClient(new BrowserHttpMessageHandler())
            {
                BaseAddress = new Uri(new BrowserUriHelper().GetBaseUriPrefix())
            };

            var httpResponse = await httpClient.PostAsync(url, content);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// posts data to a url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="token"></param>
        /// <param name="includeBearer"></param>
        /// <returns></returns>
        public async Task<string> Post(string url, 
            AuthenticationToken token, 
            Dictionary<string, string> postData = null, 
            Dictionary<string, string> headers = null, 
            bool includeBearer = true)
        {
            HttpContent content = null;
            if (postData != null)
                content = new FormUrlEncodedContent(postData.ToArray<KeyValuePair<string, string>>());
            else
                content = null;

            var httpClient = new HttpClient(new BrowserHttpMessageHandler())
            {
                BaseAddress = new Uri(new BrowserUriHelper().GetBaseUriPrefix())
            };

            if (includeBearer)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            else
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.AccessToken);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            var httpResponse = await httpClient.PostAsync(url, content);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// posts data to a url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="token"></param>
        /// <param name="includeBearer"></param>
        /// <returns></returns>
        public async Task<string> Post(string url, AuthenticationToken token, bool includeBearer = true)
        {
            HttpContent content = new StringContent(string.Empty);

            var httpClient = new HttpClient(new BrowserHttpMessageHandler())
            {
                BaseAddress = new Uri(new BrowserUriHelper().GetBaseUriPrefix())
            };

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (includeBearer)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            else
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.AccessToken);

            var httpResponse = await httpClient.PostAsync(url, content);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// posts data to a url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="token"></param>
        /// <param name="includeBearer"></param>
        /// <returns></returns>
        public async Task<string> Post(string url, AuthenticationToken token, string jsonString, bool includeBearer = true)
        {
            HttpContent content = new StringContent(jsonString);

            var httpClient = new HttpClient(new BrowserHttpMessageHandler())
            {
                BaseAddress = new Uri(new BrowserUriHelper().GetBaseUriPrefix())
            };

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (includeBearer)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            else
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.AccessToken);

            var httpResponse = await httpClient.PostAsync(url, content);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// put data to a url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="token"></param>
        /// <param name="includeBearer"></param>
        /// <returns></returns>
        public async Task<string> Put(string url, AuthenticationToken token, string jsonString = null, bool includeBearer = true)
        {
            HttpContent content = null;

            if (jsonString != null)
                content = new StringContent(jsonString);

            var httpClient = new HttpClient(new BrowserHttpMessageHandler())
            {
                BaseAddress = new Uri(new BrowserUriHelper().GetBaseUriPrefix())
            };

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (includeBearer)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            else
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.AccessToken);

            var httpResponse = await httpClient.PutAsync(url, content);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// http delete command
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="token"></param>
        /// <param name="includeBearer"></param>
        /// <returns></returns>
        public async Task<string> Delete(string url, AuthenticationToken token, bool includeBearer = true)
        {
            var httpClient = new HttpClient(new BrowserHttpMessageHandler())
            {
                BaseAddress = new Uri(new BrowserUriHelper().GetBaseUriPrefix())
            };

            if (includeBearer)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            else
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.AccessToken);

            var httpResponse = await httpClient.DeleteAsync(url);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// http delete command
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="token"></param>
        /// <param name="includeBearer"></param>
        /// <returns></returns>
        public async Task<string> Delete(string url, AuthenticationToken token, string jsonString, bool includeBearer = true)
        {
            var httpClient = new HttpClient(new BrowserHttpMessageHandler())
            {
                BaseAddress = new Uri(new BrowserUriHelper().GetBaseUriPrefix())
            };

            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(jsonString, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url)
            };

            if (includeBearer)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            else
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.AccessToken);

            var httpResponse = await httpClient.SendAsync(request);
            return await httpResponse.Content.ReadAsStringAsync();
        }
    }
}
