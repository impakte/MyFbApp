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

namespace MyFbApp.ViewModel
{
    class FacebookViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private FacebookProfile _facebookProfile;
        private FacebookUserPosts _facebookUserPosts;
        private string _facebookToken;
        private FacebookServices _facebookServices;
        private readonly INavigationService _navigationService;
        public ICommand NavigateCommand { get; set; }

        private IList<PostsData> posts;
        private bool _isLoading;

        public ICommand LoadContent { get; set; }
        public ICommand GoToPostDetailsCommand => new AsyncCommand<PostsData>(GoToPostDetailCommandExecute);

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

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


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
