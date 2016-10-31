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

        public List<POST> RetrieveListOfPost(List<int> listOFUsers, int userID)
        {
            var newsFeed = pasteBookAL.RetrieveListOfPost(listOFUsers, userID);
            return newsFeed;
        }

        public List<POST> RetrieveListOfPostOnProfile(int userID)
        {
            var timeLine = pasteBookAL.RetrieveListOfPostOnProfile(userID);
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
