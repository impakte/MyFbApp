using System.Collections.Generic;
using MyFbAppLib.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using MyFbAppLib.Services;
using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;
using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;

namespace MyFbAppLib.ViewModel
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
            _facebookPostComments = await _facebookServices.GetFacebookPostCommentPost(Id);
            this.Comments = this._facebookPostComments.Data;
        }
    }
}
