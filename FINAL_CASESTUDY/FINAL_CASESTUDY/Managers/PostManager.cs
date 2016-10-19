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
        public USER PostOnOwnProfile(int userID)
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

        public List<Post> DisplayListOfPostOnHome(List<User> listOfUsers, int userID)
        {
            List<Post> listOfPost = new List<Post>();
            List<Like> listOfLikes = new List<Like>();

              var users = new List<USER>();
            foreach (var user in listOfUsers)
            {
                users.Add(Mapper.ToUSERFromDB(user));
            }

            var posts = postBL.RetrieveListOfPost(users, userID);
            foreach (var item in posts)
            {

                listOfPost.Add(Mapper.ToListOfPost(item, listOfLikes));
            }
            return listOfPost;
        }

        public List<Post> DisplayListOfPostOnWall(string username)
        {
            List<Post> listOfPost = new List<Post>();
            List<Like> listOfLikes;
            var posts = postBL.RetrieveListOfPostOnProfile(username);
            
            foreach (var post in posts)
            {
                listOfLikes = new List<Like>();
                if (post.LIKEs != null)
                {
                    foreach (var like in post.LIKEs)
                    {                        
                        listOfLikes.Add(Mapper.ToLike(like));
                    }
                }
                listOfPost.Add(Mapper.ToListOfPost(post, listOfLikes));
                
            }
            return listOfPost;
        }

        public bool LikePost(int userID, int postID)
        {
            int like = postBL.LikeSpecificPost(userID, postID);
            return like != 0;
        }
    }
}