using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MyFbApp.DataBaseModels;
using MyFbApp.Model;
using SQLite;

namespace MyFbApp.Sqlite
{

    class DatabaseManager
    {
        private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "MyDB.db3");
        private SQLiteConnection _dbConnection = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "MyDB.db3"));

        public DatabaseManager()
        {
        }

        public void CreateTables()
        {
            _dbConnection.CreateTable<FacebookProfileDb>();
            _dbConnection.CreateTable<FacebookPostsDb>();
            //_dbConnection.CreateTable<FacebookPostCommentsDb>();
        }

        public void UpdateData(FacebookProfile profile, List<PostsData> posts)
        {
            UpdateProfile(profile);
            UpdatePosts(posts);
        }
        public void UpdateProfile(FacebookProfile profile)
        {
            _dbConnection.DeleteAll<FacebookProfileDb>();
            var maxPrimaryKey = _dbConnection.Table<FacebookProfileDb>().OrderByDescending(c => c.Id).FirstOrDefault();
            FacebookProfileDb dbprofile = new FacebookProfileDb()
            {
                Id = (maxPrimaryKey == null ? 1 : maxPrimaryKey.Id + 1),
                Name = profile.Name,
                TimeStamp = DateTime.Now
            };

            _dbConnection.Insert(dbprofile);
        }

        public void UpdatePosts(List<PostsData> posts)
        {
            _dbConnection.DeleteAll<FacebookPostsDb>();
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
                    Created_time = post.Created_time
                };
                _dbConnection.Insert(dbposts);

            }
        }

        public FacebookProfileDb getFacebookProfileDb()
        {
            FacebookProfileDb fbProfile = _dbConnection.Table<FacebookProfileDb>().First();
            return (fbProfile);
        }

        public List<FacebookPostsDb> getFacebookPostsDb()
        {
            List<FacebookPostsDb> fbPosts = _dbConnection.Table<FacebookPostsDb>().ToList();
            return fbPosts;
        }

    }
}
