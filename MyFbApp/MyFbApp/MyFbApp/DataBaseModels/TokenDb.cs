using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFbApp.DataBaseModels
{
    public class TokenDb
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string LongLiveToken { get; set; }
    }
}
