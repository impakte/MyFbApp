using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices.MVVM;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using GalaSoft.MvvmLight.Command;
using MyFbAppLib.DataBaseModels;
using MyFbAppLib.Logic;
using GalaSoft.MvvmLight.Messaging;
using MyFbAppLib.Navigation;

namespace MyFbAppLib.ViewModel
{
    public class FacebookViewModel : BaseViewModel
    {
        private FacebookProfileDb _facebookProfileDb;
        private readonly INavigationService _navigationService;
        private IList<FacebookPostsDb> _postsDb;
        private UserProfileLogic _profilLogic;

        public ICommand LoadContent { get; set; }
        public ICommand NavigateCommand { get; set; }
        public ICommand GoToPostDetailsCommand => new RelayCommand<FacebookPostsDb>(GoToPostDetailCommandExecute);

        public ICommand GoToLoginCommand => new RelayCommand(GoToLoginCommandExecute);
        public IList<FacebookPostsDb> PostsDb
        {
            get { return _postsDb; }
            set { Set(ref _postsDb, value); }
        }

        public FacebookProfileDb FacebookProfileDb
        {
            get { return _facebookProfileDb; }
            set { Set(ref _facebookProfileDb, value); }
        }

        public FacebookViewModel(INavigationService navigationService)
        {
            if (navigationService == null) throw new ArgumentNullException("navigationService");
            _navigationService = navigationService;

            this.LoadContent = new AsyncCommand(() => SetData());
            this._profilLogic = SimpleIoc.Default.GetInstance<UserProfileLogic>();
            Messenger.Default.Register<NotificationMessage>(this, SetDataUpdated);
        }

        private async void SetDataUpdated(NotificationMessage msg)
        {
            switch (msg.Notification)
            {
                case "ProfileUpdated":
                    FacebookProfileDb = await _profilLogic.GetProfileFromDb();
                    break;
                case "UserPostUpdated":
                    PostsDb = await _profilLogic.GetPostFromDb();
                    break;
            }
        }

        public async Task SetData()
        {
            IsLoading = true;
            FacebookProfileDb = await _profilLogic.GetProfileData();
            PostsDb = await _profilLogic.GetPostData();
            IsLoading = false;
        }

        public void GoToPostDetailCommandExecute(FacebookPostsDb data)
        {
            NavigateCommand = new RelayCommand(() => { _navigationService.NavigateTo(Locator.FacebookPostPage, data); });
            NavigateCommand.Execute(null);
        }

        public void GoToLoginCommandExecute()
        {
            _profilLogic.DeleteToken();
            NavigateCommand = new RelayCommand(() => { _navigationService.NavigateTo(Locator.LoginPage); });
            NavigateCommand.Execute(null);
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new AsyncCommand(async () =>
                {
                    IsRefreshing = true;

                    await SetData();

                    IsRefreshing = false;
                });
            }
        }
    }
}
