using System;
using System.Collections.Generic;
using System.Text;
using MyFbApp.DataBaseModels;
using MyFbApp.Sqlite;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System.Threading.Tasks;
using MyFbApp.Model;
using MyFbApp.Services;

namespace MyFbApp.Logic
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

        private async Task UpdateProfileApi()
        {
            await SetFacebookUserProfileAsync();
            _dbmanager.UpdateProfile(_facebookProfile);
        }

        private async Task UpdateUserPostsApi()
        {
            await SetFacebookUserPostsAsync();
            _dbmanager.UpdatePosts(_facebookUserPosts.Data);
        }

        public async Task<FacebookProfileDb> GetProfileData()
        {
            if (!await CheckUserProfileData())
                await UpdateProfileApi();
            FacebookProfileDb FbProfile = await _dbmanager.getFacebookProfileDb();
            return (FbProfile);
        }

        public async Task<List<FacebookPostsDb>> GetPostData()
        {
            if (!await CheckUserPostData())
                await UpdateUserPostsApi();
            List<FacebookPostsDb> FbPosts = await _dbmanager.getFacebookPostsDb();
            return (FbPosts);
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

        public void DeleteToken()
        {
            _dbmanager.DeleteToken();
        }
    }
}
