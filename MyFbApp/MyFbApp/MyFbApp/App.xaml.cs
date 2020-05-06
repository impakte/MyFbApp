using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyFbApp.View;
using MyFbApp.Navigation;
using GalaSoft.MvvmLight.Ioc;
using MyFbApp.Services;
using GalaSoft.MvvmLight.Views;
using MyFbApp.Configuration;
using MyFbApp.Sqlite;
using MyFbApp.Logic;

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
            
            var configLoader = new ConfigurationLoader();
            configLoader.LoadConfig();

            SimpleIoc.Default.Register<DatabaseManager>();
            //DatabaseManager dbmanager = SimpleIoc.Default.GetInstance<DatabaseManager>();
            //DANS LE ONSTART
            //dbmanager.CreateTables();

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
            //DANS LE ONSTART
            dbmanager.CreateTables();
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
