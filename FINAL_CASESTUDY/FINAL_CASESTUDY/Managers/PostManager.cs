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

        public bool CreatePost(string post, int userID, int profileOwnerID)
        {
            var userPost = Mapper.ToPOSTFromDB(post, userID, profileOwnerID);
            int result = postBL.AddPost(userPost);
            return result != 0;
        }

        public List<Post> DisplayListOfPostOnHome(List<User> listOfUsers, int userID)
        {
            List<Post> listOfPost = new List<Post>();
            var users = new List<USER>();

            foreach (var user in listOfUsers)
            {
                users.Add(Mapper.ToUSERFromDB(user));
            }

            var posts = postBL.RetrieveListOfPost(users, userID);
            foreach (var item in posts)
            {
                listOfPost.Add(Mapper.ToPost(item));
            }
            return listOfPost;
        }

        public List<Post> DisplayListOfPostOnWall(int userID)
        {
            List<Post> listOfPost = new List<Post>();
            var posts = postBL.RetrieveListOfPostOnProfile(userID);
            
            foreach (var post in posts)
            {             
                listOfPost.Add(Mapper.ToPost(post));
                
            }
            return listOfPost;
        }

        public List<User> DisplayListOfLikers(int postID)
        {
            List<User> listOfLikers = new List<User>();
            var likers = postBL.DisplayLikers(postID);

            foreach (var user in likers)
            {
                listOfLikers.Add(Mapper.ToUser(user));
            }
            return listOfLikers;
        }

        public bool LikePost(int userID, int postID)
        {
            int like = postBL.UserLikePost(userID, postID);
            return like != 0;
        }

        public bool CommentPost(int userID, int postID, string comment)
        {
            int like = postBL.UserCommentPost(userID, postID, comment);
            return like != 0;
        }
    }
}