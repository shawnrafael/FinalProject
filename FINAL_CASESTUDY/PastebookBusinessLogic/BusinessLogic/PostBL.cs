using DataAccess.PasteBookAccessLayer;
using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic.BusinessLogic
{
    public class PostBL
    {
        DataAccess<POST> accessPost = new DataAccess<POST>();
        DataAccess<USER> accessUser = new DataAccess<USER>();
        DataAccess<LIKE> accessLike = new DataAccess<LIKE>();

        DataAccess<COMMENT> accessComment = new DataAccess<COMMENT>();

        public List<POST> RetrieveListOfPost(List<USER> listOFUsers, int userID)
        {
            var listOfPost = accessPost.EntityList();
            var newsFeed = new List<POST>();

            foreach (var item in listOfPost)
            {
                if (listOFUsers.Any(x => (x.ID == item.POSTER_ID || userID == item.POSTER_ID)))
                {
                    newsFeed.Add(item);
                }
            }

            return newsFeed;
        }

        //public List<POST> RetrieveListOfPost(List<USER> listOFUsers, int userID)
        //{
        //    var listOfPost = accessPost.EntityList();
        //    var newsFeed = new List<POST>();

        //    foreach (var item in listOfPost)
        //    {
        //        if (listOFUsers.Any(x => (x.ID == item.POSTER_ID || userID == item.POSTER_ID )))
        //        {
        //            newsFeed.Add(item);
        //        }                
        //    }
                        
        //    return newsFeed;
        //}

        public List<POST> RetrieveListOfPostOnProfile(int userID)
        {
            var listOfPost = accessPost.EntityList();
            var timeLine = listOfPost.Where(x => x.PROFILE_OWNER_ID == userID).ToList();

            return timeLine;
        }

        public bool AddPost(string content, int userID, int postID)
        {
            POST newPost = new POST()
            {
                CONTENT = content,
                POSTER_ID = userID,
                PROFILE_OWNER_ID = postID,
                CREATED_DATE = DateTime.Now
            }; 
            
            bool status = accessPost.Create(newPost);
            return status;
        }
        
    }
}
