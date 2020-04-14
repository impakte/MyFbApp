using System.Collections.Generic;
using MyFbApp.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using MyFbApp.Services;
using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;
using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;

namespace MyFbApp.ViewModel
{
    public class PostDetailViewModel : BaseViewModel
    {
        
        private FacebookPostComments _facebookPostComments;
        private string _id;
        private FacebookServices _facebookServices;
        public ICommand LoadPostsCommand { get; set; }
        public string Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        private IList<CommentData> _comments;

        public IList<CommentData> Comments
        {
            get { return _comments; }
            set { Set(ref _comments, value); }
        }

        public FacebookPostComments FacebookPostComment
        {
            get { return _facebookPostComments; }
            set { Set(ref _facebookPostComments, value); }
        }

        public PostDetailViewModel()
        {
            this.Comments = new ObservableCollection<CommentData>();
            this._facebookServices = SimpleIoc.Default.GetInstance<FacebookServices>();
            this.LoadPostsCommand = new AsyncCommand(() => SetFacebookPostCommentAsync());
        }

        public async Task SetFacebookPostCommentAsync()
        {
            var fbprofileviewmodel = SimpleIoc.Default.GetInstance<FacebookViewModel>();

            _facebookPostComments = await _facebookServices.GetFacebookPostCommentPost(Id);
            this.Comments = this._facebookPostComments.Data;
        }
    }
}
