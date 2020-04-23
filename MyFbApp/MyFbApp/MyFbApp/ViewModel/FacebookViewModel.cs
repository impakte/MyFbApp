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
using MyFbApp.Sqlite;
using MyFbApp.DataBaseModels;

namespace MyFbApp.ViewModel
{
    class FacebookViewModel : BaseViewModel
    {
        private FacebookProfile _facebookProfile;
        private FacebookProfileDb _facebookProfileDb;
        private FacebookUserPosts _facebookUserPosts;
        private string _facebookToken;
        private FacebookServices _facebookServices;
        private readonly INavigationService _navigationService;
        private IList<PostsData> _posts;
        private IList<FacebookPostsDb> _postsDb;
        private int _commentsNumber;
        private FacebookPostComments _facebookPostComments;

        public ICommand LoadContent { get; set; }
        public ICommand NavigateCommand { get; set; }
        public ICommand GoToPostDetailsCommand => new Command<PostsData>(GoToPostDetailCommandExecute);

        public FacebookPostComments FacebookPostComment
        {
            get { return _facebookPostComments; }
            set { Set(ref _facebookPostComments, value); }
        }
        
        public int CommentsNumber
        {
            get { return _commentsNumber; }
            set { Set(ref _commentsNumber, value); }
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

        public IList<FacebookPostsDb> PostsDb
        {
            get { return _postsDb; }
            set { Set(ref _postsDb, value); }
        }

        public FacebookProfile FacebookProfile
        {
            get { return _facebookProfile; }
            set { Set(ref _facebookProfile, value); }
        }

        public FacebookProfileDb FacebookProfileDb
        {
            get { return _facebookProfileDb; }
            set { Set(ref _facebookProfileDb, value); }
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
            _facebookServices = SimpleIoc.Default.GetInstance<FacebookServices>();
            this.LoadContent = new AsyncCommand(() => SetFacebookProfileContent());
        }

        public void SetCacheData()
        {
            DatabaseManager dbmanager = new DatabaseManager();
            FacebookProfileDb = dbmanager.getFacebookProfileDb();
            PostsDb = dbmanager.getFacebookPostsDb();
        }

        public async Task SetFacebookProfileContent()
        {
            //IsLoading = true;
            await SetFacebookUserProfileAsync();
            await SetFacebookUserPostsAsync();
            // EN TEST
            DatabaseManager dbmanager = new DatabaseManager();
            dbmanager.UpdateData(_facebookProfile, _facebookUserPosts.Data);
            //IsLoading = false;
        }

        public async Task SetFacebookUserProfileAsync()
        {
            FacebookProfile = await _facebookServices.GetFacebookProfileAsync();
        }

        public async Task SetFacebookUserPostsAsync()
        {
            FacebookUserPosts = await _facebookServices.GetFacebookUserPosts();
            this.Posts = this._facebookUserPosts.Data;
            foreach (PostsData pd in Posts)
            {
                _facebookPostComments = await _facebookServices.GetFacebookPostCommentPost(pd.Id);
                pd.CommentsNumber = this._facebookPostComments.Data.Count;
            }
        }

        public void GoToPostDetailCommandExecute(PostsData data)
        {
            NavigateCommand = new RelayCommand(() => { _navigationService.NavigateTo(Locator.FacebookPostPage, data); });
            NavigateCommand.Execute(null);
        }

        public ICommand RefreshCommand
        {
            get
            {
                //AsyncCommand()
                return new AsyncCommand(async () =>
                {
                    IsRefreshing = true;

                    await SetFacebookUserPostsAsync();

                    IsRefreshing = false;
                });
            }
        }
    }
}
