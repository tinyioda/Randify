using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyWebApi.SpotifyModel
{
    [JsonObject]
    internal class context
    {
        public string uri { get; set; }

        public string href { get; set; }

        public external_urls external_urls { get; set; }

        public string type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Context ToPOCO()
        {
            var context = new Context();
            context.Uri = this.uri;
            context.HREF = this.href;
            context.ExternalUrls = (this.external_urls == null) ? null : this.external_urls.ToPOCO();
            context.Uri = this.uri;

            return context;
        }
    }
}
