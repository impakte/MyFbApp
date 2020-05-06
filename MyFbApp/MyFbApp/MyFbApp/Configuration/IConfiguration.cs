using System;
using System.Collections.Generic;
using System.Text;

namespace MyFbApp.Configuration
{
    public interface IConfiguration
    {
        string facebookAuthUrl { get; set; }
        string facebookRedirectUrl { get; set; }
        string facebookAppId { get; set; }
        
        string facebookAppSecret { get; set; }
        string scope { get; set; }
        string FbProfileUrl { get; set; }
        string FbUserPostUrl { get; set; }
        string FbUserPostCommentsUrl { get; set; }
    }
}
