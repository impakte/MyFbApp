using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyFbApp.Model;
using MyFbApp.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace MyFbApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FacebookPostPage : ContentPage
    {
        private readonly PostDetailViewModel viewModel;

        PostsData data;
        private PostsData Data { get { return data; } }


        public FacebookPostPage(PostsData fbpost)
        {
            InitializeComponent();
            BindingContext = this.viewModel = SimpleIoc.Default.GetInstance<PostDetailViewModel>();

            this.viewModel.Id = fbpost.Id;
            LabelMessage.Text = fbpost.Message;
            LabelDatePost.Text = "Publié le :"  + fbpost.Created_time.ToString();
            this.viewModel.LoadPostsCommand.Execute(null);
        }
    }
}