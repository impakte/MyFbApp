using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices.MVVM;
using MyFbApp.Model;
using MyFbApp.Services;

namespace MyFbApp.ViewModel
{
    class FacebookViewModel : INotifyPropertyChanged
    {
        private FacebookProfile _facebookProfile;
        private FacebookUserPosts _facebookUserPosts;
        private string _facebookToken;
        //public ICommand LoadTokenCommand { get; set; }
        public ICommand LoadContent { get; set; }
        public ICommand LoadTokenCommand { get; set; }

        private IList<PostsData> posts;

        public string FacebookToken
        {
            get { return _facebookToken; }
            set
            {
                _facebookToken = value;
                OnPropertyChanged();
            }
        }

        public IList<PostsData> Posts
        {
            get { return posts; }
            set
            {
                posts = value;
                OnPropertyChanged();
            }
        }

        public FacebookProfile FacebookProfile
        {
            get { return _facebookProfile; }
            set
            {
                _facebookProfile = value;
                OnPropertyChanged();
            }
        }

        public FacebookUserPosts FacebookUserPosts
        {
            get { return _facebookUserPosts; }
            set
            {
                _facebookUserPosts = value;
                OnPropertyChanged();
            }
        }

        public FacebookViewModel()
        {
            this.Posts = new ObservableCollection<PostsData>();

            this.LoadTokenCommand = new AsyncCommand(() => SetFacebookUserTokenAsync());
            this.LoadContent = new AsyncCommand(() => SetFacebookProfileContent());
        }

        public async Task SetFacebookProfileContent()
        {
            await SetFacebookUserProfileAsync(FacebookToken);
            await SetFacebookUserPostsAsync(FacebookToken);
        }

        public async Task SetFacebookUserTokenAsync()
        {
            var facebookServices = new FacebookServices();

            //string redirectUrl = await facebookServices.GetFacebookRedirectUrl();
            //_facebookToken = ExtractAccessTokenFromUrl(redirectUrl);
        }

        public async Task SetFacebookUserProfileAsync(string accessToken)
        {
            var facebookServices = new FacebookServices();

            FacebookProfile = await facebookServices.GetFacebookProfileAsync(accessToken);
        }

        public async Task SetFacebookUserPostsAsync(string accessToken)
        {
            var facebookServices = new FacebookServices();

            FacebookUserPosts = await facebookServices.GetFacebookUserPosts(accessToken);
            this.Posts = this._facebookUserPosts.Data;
        }

        public string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");
                var accessToken = at.Remove(at.IndexOf("&expires_in="));
                accessToken = accessToken.Remove(accessToken.IndexOf("&data_access_expiration_time="));
                return accessToken;
            }

            return string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
