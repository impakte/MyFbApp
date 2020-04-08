using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyFbApp.View;
using MyFbApp.Navigation;
using GalaSoft.MvvmLight.Ioc;
using MyFbApp.Services;
using GalaSoft.MvvmLight.Views;

namespace MyFbApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ViewModel.Bootstrap.Instance.Setup();

            var nav = new NavigationService();
            nav.Configure(Locator.LoginPage, typeof(LoginPage));
            nav.Configure(Locator.FacebookPostPage, typeof(FacebookPostPage));
            nav.Configure(Locator.FacebookProfilePage, typeof(FacebookProfilePage));
            SimpleIoc.Default.Register<INavigationService>(() => nav);

            var firstPage = new NavigationPage(new LoginPage());

            nav.Initialize(firstPage);

            //MainPage = new NavigationPage(new LoginPage());
            MainPage = firstPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
