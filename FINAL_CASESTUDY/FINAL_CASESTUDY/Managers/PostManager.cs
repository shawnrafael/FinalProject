using FINAL_CASESTUDY.Models;
using PastebookBusinessLogic.BusinessLogic;
using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FINAL_CASESTUDY.Managers
{
    public class PostManager
    {
        PostBL postBL = new PostBL();
        public USER PostOnOwnProfile(string userID)
        {
            var user = postBL.PostOnOwnProfile(userID);
            return user;
        }

        public bool CreatePost(Post post)
        {
            if (post.Content == "")
            {
                return false;
            }
            int result = postBL.AddPost(Mapper.ToPOSTFromDB(post));
            return result != 0;
        }

        public List<Post> DisplayListOfPost(int userID)
        {
            List<Post> listOfPost = new List<Post>();
            var posts = postBL.RetrieveListOfPostOnProfile(userID);
            foreach (var item in posts)
            {
                listOfPost.Add(Mapper.ToListOfPost(item));
            }
            return listOfPost;
        }
    }
}