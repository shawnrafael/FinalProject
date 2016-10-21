using DataAccess.AccessLayer;
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
        PasteBookAccessLayer pasteBookAL = new PasteBookAccessLayer();
        GenericDataAccess<POST> accessPost = new GenericDataAccess<POST>();

        public List<POST> RetrieveListOfPost(List<USER> listOFUsers, int userID)
        {
            var listOfPost = pasteBookAL.RetrieveListOfPost();
            var newsFeed = new List<POST>();

            foreach (var item in listOfPost)
            {
                if (listOFUsers.Any(x => x.ID == item.POSTER_ID || x.ID == item.PROFILE_OWNER_ID))
                {
                    newsFeed.Add(item);
                }
                else if(userID == item.POSTER_ID)
                {
                    newsFeed.Add(item);
                }
            }
            return newsFeed;
        }

        public List<POST> RetrieveListOfPostOnProfile(int userID)
        {
            var listOfPost = pasteBookAL.RetrieveListOfPost();
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
