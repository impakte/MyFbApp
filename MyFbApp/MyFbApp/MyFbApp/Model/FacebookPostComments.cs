using System;
using System.Collections.Generic;
using System.Text;

namespace MyFbApp.Model
{
    public class FacebookPostComments
    {

        public List<CommentData> Data { get; set; }

        public PagingDataComment Paging { get; set; }

    }

    public class CommentData
    {
        public DateTime Created_time { get; set; }

        public Publisher User { get; set; }
        public string Message { get; set; }
        public string Id { get; set; }
    }

    public class Publisher
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }

    public class PagingDataComment
    {
        public CursorComment Cursor { get; set; }
    }

    public class CursorComment
    {
        public string Before { get; set; }
        public string After { get; set; }
    }
}
