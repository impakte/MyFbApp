using System;
using System.Collections.Generic;
using System.Text;
using MyFbApp.Services;
using MyFbApp.View;
using Xamarin.Auth;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using MyFbApp.Navigation;
using MyFbApp.Configuration;
using AsyncAwaitBestPractices.MVVM;
using System.Threading.Tasks;
using MyFbApp.Logic;

namespace MyFbApp.ViewModel
{
    class LoginPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IConfiguration _config;
        private LoginLogic _loginLogic;
        public ICommand NavigateCommand { get; set; }
        public OAuth2Authenticator Authenticator;
        public ICommand ConnectVerification { get; set; }
        public bool CanSkipPage { get; set; }
       


        public LoginPageViewModel(INavigationService navigationService, IConfiguration configuration)
        {
            if (navigationService == null) throw new ArgumentNullException("navigationService");
            _navigationService = navigationService;
            if (configuration == null) throw new ArgumentNullException("Configuration");
            _config = configuration;

            NavigateCommand = new RelayCommand(() => { _navigationService.NavigateTo(Locator.FacebookProfilePage); });
            Authenticator = new OAuth2Authenticator(
                 _config.facebookAppId,
                 _config.scope,
                 new Uri(_config.facebookAuthUrl),
                 new Uri(_config.facebookRedirectUrl),
                 null);
            Authenticator.Completed += OnAuthenticationCompleted;
            Authenticator.Error += OnAuthenticationFailed;
            this._loginLogic = SimpleIoc.Default.GetInstance<LoginLogic>();
            this.ConnectVerification = new AsyncCommand(() => TokenVerification());
        }

        public async Task TokenVerification()
        {
            IsLoading = true;
            if (await _loginLogic.getToken())
                NavigateCommand.Execute(null);
            IsLoading = false;
        }

        async void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            IsLoading = true;
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthenticationCompleted;
                authenticator.Error -= OnAuthenticationFailed;
            }
            await _loginLogic.SetTokenAsync(e.Account.Properties["access_token"]);
            NavigateCommand.Execute(null);
            IsLoading = false;
        }

        void OnAuthenticationFailed(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthenticationCompleted;
                authenticator.Error -= OnAuthenticationFailed;
            }
        }
    }

}
