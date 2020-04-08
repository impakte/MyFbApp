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

namespace MyFbApp.ViewModel
{
    class LoginPageViewModel
    {
        private readonly INavigationService _navigationService;
        public ICommand NavigateCommand { get; set; }
        public OAuth2Authenticator Authenticator;

        public LoginPageViewModel(INavigationService navigationService)
        {
            if (navigationService == null) throw new ArgumentNullException("navigationService");
            _navigationService = navigationService;

            NavigateCommand = new RelayCommand(() => { _navigationService.NavigateTo(Locator.FacebookProfilePage); });

            Authenticator = new OAuth2Authenticator(
                 "545621829700082",
                 "email",
                 new Uri("https://www.facebook.com/dialog/oauth/"),
                 new Uri("https://www.facebook.com/connect/login_success.html"),
                 null);
            Authenticator.Completed += OnAuthenticationCompleted;
            Authenticator.Error += OnAuthenticationFailed;
        }

        void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthenticationCompleted;
                authenticator.Error -= OnAuthenticationFailed;
            }
            SimpleIoc.Default.GetInstance<FacebookServices>().SetAccessToken(e.Account.Properties["access_token"]);
            NavigateCommand.Execute(null);
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
