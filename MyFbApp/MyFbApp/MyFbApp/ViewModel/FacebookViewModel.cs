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
using GalaSoft.MvvmLight.Views;
using System;
using GalaSoft.MvvmLight.Command;
using MyFbApp.Navigation;
using Xamarin.Forms;

namespace MyFbApp.ViewModel
{
    class FacebookViewModel : BaseViewModel
    {
        private FacebookProfile _facebookProfile;
        private FacebookUserPosts _facebookUserPosts;
        private string _facebookToken;
        private FacebookServices _facebookServices;
        private readonly INavigationService _navigationService;
        private IList<PostsData> _posts;
        private bool _isLoading;

        public ICommand LoadContent { get; set; }
        public ICommand NavigateCommand { get; set; }
        public ICommand GoToPostDetailsCommand => new AsyncCommand<PostsData>(GoToPostDetailCommandExecute);

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { Set(ref _isRefreshing, value); }
        }

       public bool IsLoading
        {
            get { return _isLoading; }
            set { Set(ref _isLoading, value); }
        }

        public string FacebookToken
        {
            get { return _facebookToken; }
            set { Set(ref _facebookToken, value); }
        }

        public IList<PostsData> Posts
        {
            get { return _posts; }
            set { Set(ref _posts, value); }
        }

        public FacebookProfile FacebookProfile
        {
            get { return _facebookProfile; }
            set { Set(ref _facebookProfile, value); }
        }

        public FacebookUserPosts FacebookUserPosts
        {
            get { return _facebookUserPosts; }
            set { Set(ref _facebookUserPosts, value); }
        }

        public FacebookViewModel(INavigationService navigationService)
        {
            if (navigationService == null) throw new ArgumentNullException("navigationService");
            _navigationService = navigationService;

            this.Posts = new ObservableCollection<PostsData>();
            this._facebookServices = SimpleIoc.Default.GetInstance<FacebookServices>();
            this.LoadContent = new AsyncCommand(() => SetFacebookProfileContent());
        }

        public async Task SetFacebookProfileContent()
        {
            IsLoading = true;
            await SetFacebookUserProfileAsync();
            await SetFacebookUserPostsAsync();
            IsLoading = false;
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

        public async Task GoToPostDetailCommandExecute(PostsData Data)
        {
            NavigateCommand = new RelayCommand(() => { _navigationService.NavigateTo(Locator.FacebookPostPage, Data); });
            NavigateCommand.Execute(null);
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await SetFacebookUserPostsAsync();

                    IsRefreshing = false;
                });
            }
        }
    }
}
