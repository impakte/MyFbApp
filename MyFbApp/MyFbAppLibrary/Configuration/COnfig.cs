using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFbAppLib.Configuration
{
    public class Config : IConfiguration
    {
        public string facebookAuthUrl { get; set; }
        public string facebookRedirectUrl { get; set; }
        public string facebookAppId { get; set; }
        public string facebookAppSecret { get; set; }
        public string scope { get; set; }
        public string FbProfileUrl { get; set; }
        public string FbUserPostUrl { get; set; }
        public string FbUserPostCommentsUrl { get; set; }

        [JsonConstructor]
        public Config()
        {
        }


    }
}
