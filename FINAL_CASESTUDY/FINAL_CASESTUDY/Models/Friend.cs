using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FINAL_CASESTUDY.Models
{
    public class Friend
    {
        public int ID { get; set; }
        public int USER_ID { get; set; }
        public int FRIEND_ID { get; set; }
        public string REQUEST { get; set; }
        public string BLOCKED { get; set; }
        public DateTime CREATED_DATE { get; set; }
    }
}