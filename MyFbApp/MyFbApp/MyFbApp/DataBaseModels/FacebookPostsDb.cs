using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MyFbApp.DataBaseModels
{
    public class FacebookPostsDb
    {
        [PrimaryKey]
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }
        public DateTime Created_time { get; set; }
        public string PostsId { get; set; }
        public int CommentsNumber { get; set; }
        public string UserId { get; set; }
    }
}
