using System;
using Xamarin.Forms;
using MyFbApp.View;
using MyFbApp.Navigation;
using MyFbAppLib.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MyFbAppLib.Sqlite;
using MyFbAppLib.Logic;
using MyFbAppLib.Navigation;

using MyFbAppFSharp;

namespace MyFbApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Bootstrap.Instance.Setup();

            var nav = new NavigationService();
            nav.Configure(Locator.LoginPage, typeof(LoginPage));
            nav.Configure(Locator.FacebookPostPage, typeof(FacebookPostPage));
            nav.Configure(Locator.FacebookProfilePage, typeof(FacebookProfilePage));
            SimpleIoc.Default.Register<INavigationService>(() => nav);
            SimpleIoc.Default.Register<DatabaseManager>();
            SimpleIoc.Default.Register<UserProfileLogic>();
            SimpleIoc.Default.Register<LoginLogic>();
            var firstPage = new NavigationPage(new LoginPage());
            nav.Initialize(firstPage);
            MainPage = firstPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            DatabaseManager dbmanager = SimpleIoc.Default.GetInstance<DatabaseManager>();
            dbmanager.CreateTables();
            //databaseManager.CreateTables();
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
