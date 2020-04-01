using System;
using System.Collections.Generic;
using System.Text;

namespace MyFbApp.Model
{
    public class FacebookUserPosts
    {
        public PostsData[] Data { get; set; }
        public PagingDataPost Paging { get; set; }
        

    }

    public class PostsData
    {
        public string Message { get; set; }
        public DateTime Created_time { get; set; }
        public string Id { get; set; }
    }

    public class PagingDataPost
    {
        public string Previous { get; set; }
        public string Next { get; set; }
    }
}

