
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MyFbAppLib.Model;
using MyFbAppLib.Sqlite;
using GalaSoft.MvvmLight.Ioc;
using MyFbAppLib.Configuration;

namespace MyFbAppLib.Services
{
    class FacebookServices
    {
        private string _accessToken;
        private DatabaseManager _dbmanager;
        private HttpClient _httpCLient = new HttpClient();
        private readonly IConfiguration _config;


        public FacebookServices(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("Configuration");
            _config = configuration;
            _dbmanager = SimpleIoc.Default.GetInstance<DatabaseManager>();
        }

        public async Task<FacebookProfile> GetFacebookProfileAsync()
        {
            var requestUrl =
                "https://graph.facebook.com/me/?fields=id,name,picture,cover,age_range,birthday,email,first_name,last_name&access_token="
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

        public async Task<bool> CheckTokenValidity()
        {
            var requestUrl = "https://graph.facebook.com/me?access_token=" + _dbmanager.getLongLiveToken();

            var checkJson = await _httpCLient.GetStringAsync(requestUrl);
            
            if (checkJson.Contains("name"))
            {
                _accessToken = _dbmanager.getLongLiveToken();
                return true;
            }
            else
                return false;
        }

        public async Task<Token> getLongLiveToken(string token)
        {
            var requestUrl = "https://graph.facebook.com/oauth/access_token?client_id=" + _config.facebookAppId + 
                "&client_secret=" + _config.facebookAppSecret + 
                "&grant_type=fb_exchange_token&fb_exchange_token=" + token;

            var tokenJson = await _httpCLient.GetStringAsync(requestUrl);

            var newToken = await Task.Run(() => JsonConvert.DeserializeObject<Token>(tokenJson));

            _accessToken = newToken.access_token;
            return (newToken);
        }
    }
}
