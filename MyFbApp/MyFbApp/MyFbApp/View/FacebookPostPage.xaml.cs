using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyFbAppLib.Model;
using MyFbAppLib.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MyFbAppLib.DataBaseModels;

namespace MyFbApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FacebookPostPage : ContentPage
    {
        private readonly PostDetailViewModel viewModel;


        public FacebookPostPage(FacebookPostsDb fbpost)
        {
            InitializeComponent();
            BindingContext = this.viewModel = SimpleIoc.Default.GetInstance<PostDetailViewModel>();

            this.viewModel.Id = fbpost.PostsId;
            LabelMessage.Text = fbpost.Message;
            LabelDatePost.Text = "Publié le :"  + fbpost.Created_time.ToString();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.viewModel.LoadPostsCommand.Execute(null);
        }
    }
}