using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices.MVVM;
using MyFbApp.Model;
using MyFbApp.Services;
using GalaSoft.MvvmLight.Ioc;

namespace MyFbApp.ViewModel
{
    class FacebookViewModel : INotifyPropertyChanged
    {
        private FacebookProfile _facebookProfile;
        private FacebookUserPosts _facebookUserPosts;
        private string _facebookToken;
        private FacebookServices _facebookServices;
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
            this._facebookServices = SimpleIoc.Default.GetInstance<FacebookServices>();
            this.LoadContent = new AsyncCommand(() => SetFacebookProfileContent());
        }

        public async Task SetFacebookProfileContent()
        {
            await SetFacebookUserProfileAsync();
            await SetFacebookUserPostsAsync();
        }

        public async Task SetFacebookUserProfileAsync()
        {
            FacebookProfile = await _facebookServices.GetFacebookProfileAsync();
        }

        public async Task SetFacebookUserPostsAsync()
        {
            FacebookUserPosts = await _facebookServices.GetFacebookUserPosts();
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
