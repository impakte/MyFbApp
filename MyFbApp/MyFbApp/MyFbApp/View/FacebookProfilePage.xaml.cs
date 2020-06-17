using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFbAppLib.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyFbAppLib.Model;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MyFbAppLib.DataBaseModels;

namespace MyFbApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class FacebookProfilePage : ContentPage
    {
        private readonly FacebookViewModel _viewModel;

        public FacebookProfilePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = SimpleIoc.Default.GetInstance<FacebookViewModel>();
            Content = MainStackLayout;
        }

        private void PostsView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as FacebookPostsDb;
            var command = _viewModel.GoToPostDetailsCommand;
            command.Execute(content);
        }

        private void Search_Clicked(object sender, EventArgs args) 
        {
            _viewModel.GoToLoginCommand.Execute(null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            _viewModel.LoadContent.Execute(null);
        }
    }
}