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
        string scope { get; set; }
        string getFbProfile { get; set; }
        string getFbUserPosts { get; set; }
        string getFbUserPostComments { get; set; }
    }
}
