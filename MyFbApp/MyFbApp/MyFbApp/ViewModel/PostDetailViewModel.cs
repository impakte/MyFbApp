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
    public class PostDetailViewModel : INotifyPropertyChanged
    {
        public ICommand LoadPostsCommand { get; set; }
        private FacebookPostComments _facebookPostComments;
        private string _id;
        private FacebookServices _facebookServices;

        public string Id
        {
            get { return _id; }
            set 
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private IList<CommentData> _comments;

        public IList<CommentData> Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                OnPropertyChanged();
            }
        }

        public FacebookPostComments FacebookPostComment
        {
            get { return _facebookPostComments; }
            set
            {
                _facebookPostComments = value;
                OnPropertyChanged();
            }
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
