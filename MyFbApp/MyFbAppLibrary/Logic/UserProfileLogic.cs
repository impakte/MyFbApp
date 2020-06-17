using System.Collections.Generic;
using MyFbAppLib.DataBaseModels;
using MyFbAppLib.Sqlite;
using GalaSoft.MvvmLight.Ioc;
using System.Threading.Tasks;
using MyFbAppLib.Model;
using MyFbAppLib.Services;
using GalaSoft.MvvmLight.Messaging;
using AsyncAwaitBestPractices;

namespace MyFbAppLib.Logic
{
    public class UserProfileLogic
    {

        private DatabaseManager _dbmanager;
        private FacebookProfile _facebookProfile;
        private FacebookUserPosts _facebookUserPosts;
        private FacebookPostComments _facebookPostComments;
        private int _commentsNumber;
        private List<PostsData> _posts;
        private FacebookServices _facebookServices;

        public FacebookProfile FacebookProfile 
        {
            get { return _facebookProfile; }
            set { _facebookProfile = value; }
        }

        public FacebookPostComments FacebookPostComment
        {
            get { return _facebookPostComments; }
            set { _facebookPostComments = value; }
        }

        public int CommentsNumber
        {
            get { return _commentsNumber; }
            set { _commentsNumber = value; }
        }

        public List<PostsData> Posts
        {
            get { return _posts; }
            set { _posts = value; }
        }

        public FacebookUserPosts FacebookUserPosts
        {
            get { return _facebookUserPosts; }
            set { _facebookUserPosts = value; }
        }

        public UserProfileLogic()
        {
            _dbmanager = SimpleIoc.Default.GetInstance<DatabaseManager>();
            _facebookServices = SimpleIoc.Default.GetInstance<FacebookServices>();
        }

        //Method to check if there is Data in cache that are usable (checking TimeStamp and User) 
        private async Task<bool> CheckUserProfileData()
        {
            await SetFacebookUserProfileAsync();
            return (_dbmanager.CheckUser(_facebookProfile.Id) && _dbmanager.CheckTimeStamp(_facebookProfile.Id));
        }

        private async Task<bool> CheckUserPostData()
        {
            return (_dbmanager.CheckPostUser(_facebookProfile.Id) && _dbmanager.CheckTimeStamp(_facebookProfile.Id));
        }

        private async Task UpdateProfileApi(bool needSendMsg)
        {
            await SetFacebookUserProfileAsync();
            _dbmanager.UpdateProfile(_facebookProfile);
            if (needSendMsg)
            {
                var myMessage = new NotificationMessage("ProfileUpdated");
                Messenger.Default.Send(myMessage);
            }
        }

        private async Task UpdateUserPostsApi(bool needSendMsg)
        {
            await SetFacebookUserPostsAsync();
            _dbmanager.UpdatePosts(_facebookUserPosts.Data);
            if (needSendMsg)
            {
                var myMessage = new NotificationMessage("UserPostUpdated");
                Messenger.Default.Send(myMessage);
            }
        }

        public async Task<FacebookProfileDb> GetProfileData()
        {
            if (await CheckUserProfileData())
            {
                FacebookProfileDb FbProfile = await _dbmanager.getFacebookProfileDb();
                UpdateProfileApi(true).SafeFireAndForget();
                return (FbProfile);
            }
            else
            {
                await UpdateProfileApi(false);
                FacebookProfileDb FbProfile = await _dbmanager.getFacebookProfileDb();
                return (FbProfile);
            }
        }

        public async Task<List<FacebookPostsDb>> GetPostData()
        {
            if (await CheckUserPostData())
            {
                List<FacebookPostsDb> FbPosts = await _dbmanager.getFacebookPostsDb();
                UpdateUserPostsApi(true).SafeFireAndForget();
                return (FbPosts);
            }
            else
            {
                await UpdateUserPostsApi(false);
                List<FacebookPostsDb> FbPosts = await _dbmanager.getFacebookPostsDb();
                return (FbPosts);
            }
        }

        private async Task SetFacebookUserProfileAsync()
        {
            FacebookProfile = await _facebookServices.GetFacebookProfileAsync();
        }
        
        private async Task SetFacebookUserPostsAsync()
        {
            FacebookUserPosts = await _facebookServices.GetFacebookUserPosts();
            this.Posts = _facebookUserPosts.Data;
            foreach (PostsData pd in Posts)
            {
                _facebookPostComments = await _facebookServices.GetFacebookPostCommentPost(pd.Id);
                pd.CommentsNumber = _facebookPostComments.Data.Count;
            }
        }

        public async Task<FacebookProfileDb> GetProfileFromDb()
        {
            return (await _dbmanager.getFacebookProfileDb());
        }

        public async Task<List<FacebookPostsDb>> GetPostFromDb()
        {
            return (await _dbmanager.getFacebookPostsDb());
        }

        public void DeleteToken()
        {
            _dbmanager.DeleteToken();
        }
    }
}
