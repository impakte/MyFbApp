using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MyFbAppLib.DataBaseModels;
using MyFbAppLib.Model;
using SQLite;

namespace MyFbAppLib.Sqlite
{

    public class DatabaseManager
    {
        private SQLiteConnection _dbConnection = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "MyDB.db3"));

        public DatabaseManager()
        {
        }

        public void CreateTables()
        {
            _dbConnection.CreateTable<FacebookProfileDb>();
            _dbConnection.CreateTable<FacebookPostsDb>();
            _dbConnection.CreateTable<TokenDb>();
        }

        public bool CheckToken()
        {
            return (_dbConnection.Table<TokenDb>().Count() > 0);
        }

        public string getLongLiveToken()
        {
            return (_dbConnection.Table<TokenDb>().First().LongLiveToken);
        }

        public void UpdateLongLiveToken(string token)
        {
            _dbConnection.DeleteAll<TokenDb>();
            var maxPrimaryKey = _dbConnection.Table<TokenDb>().OrderByDescending(c => c.Id).FirstOrDefault();
            TokenDb tokens = new TokenDb
            {
                Id = (maxPrimaryKey == null ? 1 : maxPrimaryKey.Id + 1),
                LongLiveToken =token
            };
            _dbConnection.Insert(tokens);
        }

        public void UpdateProfile(FacebookProfile profile)
        {
            if (CheckUser(profile.Id))
                _dbConnection.Execute("DELETE FROM FacebookProfileDb WHERE UserId = ?", profile.Id);
            var maxPrimaryKey = _dbConnection.Table<FacebookProfileDb>().OrderByDescending(c => c.Id).FirstOrDefault();
            FacebookProfileDb dbprofile = new FacebookProfileDb()
            {
                Id = (maxPrimaryKey == null ? 1 : maxPrimaryKey.Id + 1),
                Name = profile.Name,
                TimeStamp = DateTime.Now,
                UserId = profile.Id
            };

            _dbConnection.Insert(dbprofile);
        }

        public void UpdatePosts(List<PostsData> posts)
        {
            string UserId = posts[0].Id.Substring(0, 16);
            if (CheckPostUser(UserId))
                _dbConnection.Execute("DELETE FROM FacebookPostsDb WHERE UserId = ?", UserId);
            foreach (PostsData post in posts)
            {
                var maxPrimaryKey = _dbConnection.Table<FacebookPostsDb>().OrderByDescending(c => c.Id).FirstOrDefault();
                FacebookPostsDb dbposts = new FacebookPostsDb()
                {
                    Id = (maxPrimaryKey == null ? 1 : maxPrimaryKey.Id + 1),
                    Message = post.Message,
                    TimeStamp = DateTime.Now,
                    CommentsNumber = post.CommentsNumber,
                    PostsId = post.Id,
                    Created_time = post.Created_time,
                    UserId = UserId
                };
                _dbConnection.Insert(dbposts);
            }
        }

        public async Task<FacebookProfileDb> getFacebookProfileDb()
        {
            FacebookProfileDb fbProfile = await Task.Run(() => _dbConnection.Table<FacebookProfileDb>().First());
            return fbProfile;
        }

        public async Task<List<FacebookPostsDb>> getFacebookPostsDb()
        {
            List<FacebookPostsDb> fbPosts = await Task.Run(() => _dbConnection.Table<FacebookPostsDb>().ToList());
            return fbPosts;
        }

        public bool CheckTimeStamp(string UserId)
        {
            return ((DateTime.Compare(_dbConnection.Table<FacebookProfileDb>().Where(w => w.UserId == UserId).First().TimeStamp, DateTime.Now.AddDays(-5)) > 0));
        }

        public bool CheckUser(string UserId)
        {
            return (_dbConnection.Table<FacebookProfileDb>().Where(w => w.UserId == UserId).Count() > 0);
        }

        public bool CheckPostUser(string UserId)
        {
            return (_dbConnection.Table<FacebookPostsDb>().Where(w => w.UserId == UserId).Count() > 0);
        }

        public void DeleteToken()
        {
            _dbConnection.DeleteAll<TokenDb>();
        }

    }
}
