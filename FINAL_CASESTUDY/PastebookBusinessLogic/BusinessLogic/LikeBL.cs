using DataAccess.PasteBookAccessLayer;
using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic.BusinessLogic
{
    public class LikeBL
    {
        DataAccess<LIKE> accessLike = new DataAccess<LIKE>();
        DataAccess<POST> accessPost = new DataAccess<POST>();
        DataAccess<USER> accessUser = new DataAccess<USER>();

        public bool LikePost(int userID, int currentPost)
        {
            var listOfLike = accessLike.EntityList();

            LIKE like = new LIKE()
            {
                LIKED_BY = userID,
                POST_ID = currentPost
            };

            bool status = false;

            if (listOfLike.Any(x=>(x.LIKED_BY==like.LIKED_BY && x.POST_ID == like.POST_ID)) == false)
            {
                status = accessLike.Create(like);
            }
                        
            return status;
        }

        public List<USER> DisplayLikers(int postID)
        {
            var listOfUser = accessUser.EntityList();
            var listOFLikes = accessLike.EntityList();
            var listOfLikers = new List<USER>();

            foreach (var item in listOfUser)
            {
                if (listOFLikes.Any(x=>x.LIKED_BY == postID)==true)
                {
                    listOfLikers.Add(item);
                }
            }

            return listOfLikers;
        }
    }
}
