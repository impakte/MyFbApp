
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MyFbApp.Model;
using System.Net.Http.Headers;
using Xamarin.Auth;

namespace MyFbApp.Services
{
    class FacebookServices
    {
        private readonly string ClientId = "545621829700082";


        /*public async Task<string> GetFacebookToken()
        {
            OAuth2Authenticator _auth = new OAuth2Authenticator(
                    ClientId,
                    "email",
                    new Uri("https://www.facebook.com/dialog/oauth/"),
                    new Uri("https://www.facebook.com/connect/login_success.html"));

        }*/

        public async Task<FacebookProfile> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl =
                "https://graph.facebook.com/me/?fields=name,picture,work,website,religion,location,locale,link,cover,age_range,birthday,devices,email,first_name,last_name,gender,hometown,is_verified,languages&access_token="
                + accessToken;

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

            return facebookProfile;
        }

        public async Task<FacebookUserPosts> GetFacebookUserPosts(string accessToken)
        {
            var requestUrl = "https://graph.facebook.com/me/posts?access_token="
                + accessToken;

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var facebookUserPosts = JsonConvert.DeserializeObject<FacebookUserPosts>(userJson);

            return facebookUserPosts;
        }

            public async Task<FacebookPostComments> GetFacebookPostCommentPost(string Id, string accessToken)
        {
            var requestUrl = "https://graph.facebook.com/" + Id + "/comments?access_token=" + accessToken;

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var facebookpostcomments = JsonConvert.DeserializeObject<FacebookPostComments>(userJson);

            return facebookpostcomments;
        }
    }
}
