using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFbApp.Configuration
{
    public class Config : IConfiguration
    {
        public string facebookAuthUrl { get; set; }
        public string facebookRedirectUrl { get; set; }
        public string facebookAppId { get; set; }
        public string scope { get; set; }
        public string getFbProfile { get; set; }
        public string getFbUserPosts { get; set; }
        public string getFbUserPostComments { get; set; }

        [JsonConstructor]
        public Config()
        {
        }


    }
}
