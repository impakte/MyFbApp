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
        private LoginPageViewModel viewModel;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this.viewModel = SimpleIoc.Default.GetInstance<LoginPageViewModel>();
        }

        private void LoginWithFacebook_Clicked(object sender, EventArgs e)
        {
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(viewModel.Authenticator);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.viewModel.ConnectVerification.Execute(null);
        }
    }
}