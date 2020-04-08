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
using GalaSoft.MvvmLight.Views;

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
            Content = MainStackLayout;
            ProfilePicture.Source = "http://scontent-cdg2-1.xx.fbcdn.net/v/t31.0-1/cp0/c15.0.50.50a/p50x50/10733713_10150004552801937_4553731092814901385_o.jpg?_nc_cat=1&_nc_sid=12b3be&_nc_ohc=kbRb-3obZ7MAX8abHIx&_nc_ht=scontent-cdg2-1.xx&oh=e51c3f7be4930957e49faf13a62ddcc7&oe=5EB2D8AC";
        }

        //OnNavigatedTo
        private void PostsView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as PostsData;
            var command = this.viewModel.GoToPostDetailsCommand;
            command.Execute(content);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            this.viewModel.LoadContent.Execute(null);
        }
    }
}