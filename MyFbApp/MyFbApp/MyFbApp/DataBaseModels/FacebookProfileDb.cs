using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SQLite;

namespace MyFbApp.DataBaseModels
{
    class FacebookProfileDb
    {
        [PrimaryKey]
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }

        public string Name { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
    }
}
