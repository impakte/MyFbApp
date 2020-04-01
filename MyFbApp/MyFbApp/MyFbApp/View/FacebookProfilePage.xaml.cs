using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFbApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyFbApp.Model;
using GalaSoft.MvvmLight.Ioc;

namespace MyFbApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class FacebookProfilePage : ContentPage
    {
        private readonly string ClientId = "545621829700082";
        private readonly FacebookViewModel viewModel;
        private string accessToken;

        public FacebookProfilePage()
        {
            InitializeComponent();

            BindingContext = this.viewModel = SimpleIoc.Default.GetInstance<FacebookViewModel>();

            //this.viewModel.LoadTokenCommand.Execute(null);
             var apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id="
                + ClientId
                + "&display=popup&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";

             var webView = new WebView
             {
                 Source = apiRequest,
                 HeightRequest = 1
             };

             webView.Navigated += WebViewOnNavigated;

             Content = webView;

            /*this.viewModel.LoadTokenCommand.Execute(null);
            this.viewModel.LoadContent.Execute(null);
            Content = MainStackLayout;*/
        }

        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {

            
            var token = this.viewModel.ExtractAccessTokenFromUrl(e.Url);

            if (token != "")
            {
                this.accessToken = token;
                this.viewModel.FacebookToken = token;
                //this.viewModel.LoadContent.Execute(null);
                await this.viewModel.SetFacebookUserProfileAsync(token);
                await this.viewModel.SetFacebookUserPostsAsync(token);
                Content = MainStackLayout;
            }
        }

        private async void PostsView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as PostsData;
            await Navigation.PushAsync(new FacebookPostPage(content));
        }
    }
}