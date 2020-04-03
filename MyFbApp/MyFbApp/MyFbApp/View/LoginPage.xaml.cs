using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFbApp.Services;
using GalaSoft.MvvmLight.Ioc;
using MyFbApp.ViewModel;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyFbApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginWithFacebook_Clicked(object sender, EventArgs e)
        {
            var authenticator = new OAuth2Authenticator(
                 "545621829700082",
                 "email",
                 new Uri("https://www.facebook.com/dialog/oauth/"),
                 new Uri("https://www.facebook.com/connect/login_success.html"),
                 null);

            authenticator.Completed += OnAuthenticationCompleted;
            authenticator.Error += OnAuthenticationFailed;

            //AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);

        }

        async void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthenticationCompleted;
                authenticator.Error -= OnAuthenticationFailed;
            }
            SimpleIoc.Default.GetInstance<FacebookServices>().SetAccessToken(e.Account.Properties["access_token"]);
            await Navigation.PushAsync(new FacebookProfilePage());
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