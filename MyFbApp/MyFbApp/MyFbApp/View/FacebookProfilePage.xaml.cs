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
        private readonly FacebookViewModel viewModel;

        public FacebookProfilePage()
        {
            InitializeComponent();
            BindingContext = this.viewModel = SimpleIoc.Default.GetInstance<FacebookViewModel>();
            this.viewModel.LoadContent.Execute(null);
            Content = MainStackLayout;
        }

        private async void PostsView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as PostsData;
            await Navigation.PushAsync(new FacebookPostPage(content));
        }
    }
}