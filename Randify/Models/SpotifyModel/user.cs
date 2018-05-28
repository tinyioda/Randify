using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Randify.Models.SpotifyModel
{
    [JsonObject]
    internal class user
    {
        public string country { get; set; }
        public string display_name { get; set; }
        public string email { get; set; }
        public external_urls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public image[] images { get; set; }
        public string product { get; set; }
        public string type { get; set; }
        public string uri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public User ToPOCO()
        {
            var user = new User();
            user.Country = this.country;
            user.DisplayName = this.display_name;
            user.EmailAddress = this.email;
            if (external_urls != null)
                user.ExternalUrl = this.external_urls.ToPOCO();
            user.HREF = this.href;
            user.Id = this.id;
            if (this.images != null)
            {
                foreach (var image in this.images)
                {
                    var poco = image.ToPOCO();
                    if(poco != null)
                        user.Images.Add(poco);
                }
            }
            user.Product = this.product;
            user.Type = this.type;
            user.Uri = this.uri;

            return user;
        }
    }
}
