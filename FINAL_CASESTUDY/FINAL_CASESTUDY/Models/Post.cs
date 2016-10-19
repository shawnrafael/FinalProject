using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FINAL_CASESTUDY.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public int ProfileOwnerID { get; set; }
        public int PosterID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilePic { get; set; }
        public List<Like> ListOfLikes { get; set; }
    }
}