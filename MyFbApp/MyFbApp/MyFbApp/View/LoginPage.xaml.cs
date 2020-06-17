using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFbAppLib.Services;
using GalaSoft.MvvmLight.Ioc;
using MyFbAppLib.ViewModel;
using MyFbAppLib;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.FSharp.Core;
using MyFbAppFSharp;

namespace MyFbApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginPageViewModel viewModel;
        //private Test.MyClass1 viewModel;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this.viewModel = SimpleIoc.Default.GetInstance<LoginPageViewModel>();
            //BindingContext = this.viewModel = SimpleIoc.Default.GetInstance<Test.MyClass1>();
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