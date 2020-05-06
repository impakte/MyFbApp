using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SQLite;

namespace MyFbApp.DataBaseModels
{
    public class FacebookProfileDb
    {
        [PrimaryKey]
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }
    }
}
