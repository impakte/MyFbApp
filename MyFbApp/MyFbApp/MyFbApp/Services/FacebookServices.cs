
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MyFbApp.Model;

namespace MyFbApp.Services
{
    class FacebookServices
    {
        private string _accessToken;
        private HttpClient _httpCLient = new HttpClient();

        public async Task<FacebookProfile> GetFacebookProfileAsync()
        {
            var requestUrl =
                "https://graph.facebook.com/me/?fields=name,picture,cover,age_range,birthday,email,first_name,last_name&access_token="
                + _accessToken;


            var userJson = await _httpCLient.GetStringAsync(requestUrl);

            var facebookProfile = await Task.Run(() => JsonConvert.DeserializeObject<FacebookProfile>(userJson));

            return facebookProfile;
        }

        public async Task<FacebookUserPosts> GetFacebookUserPosts()
        {
            var requestUrl = "https://graph.facebook.com/me/posts?access_token="
                + _accessToken;


            var userJson = await _httpCLient.GetStringAsync(requestUrl);

            var facebookUserPosts = await Task.Run(() => JsonConvert.DeserializeObject<FacebookUserPosts>(userJson));

            return facebookUserPosts;
        }

            public async Task<FacebookPostComments> GetFacebookPostCommentPost(string Id)
        {
            var requestUrl = "https://graph.facebook.com/" + Id + "/comments?access_token=" + _accessToken;

            var userJson = await _httpCLient.GetStringAsync(requestUrl);

            var facebookpostcomments = await Task.Run(() => JsonConvert.DeserializeObject<FacebookPostComments>(userJson));

            return facebookpostcomments;
        }

        public void SetAccessToken(string Token)
        {
            _accessToken = Token;
        }
    }
}
