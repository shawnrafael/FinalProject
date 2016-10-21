using DataAccess.AccessLayer;
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
        GenericDataAccess<LIKE> accessLike = new GenericDataAccess<LIKE>();
        GenericDataAccess<POST> accessPost = new GenericDataAccess<POST>();
        GenericDataAccess<USER> accessUser = new GenericDataAccess<USER>();

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

                if (listOFLikes.Any(x=>(x.LIKED_BY == item.ID && x.POST_ID==postID))==true)
                {
                    listOfLikers.Add(item);
                }
            }

            return listOfLikers;
        }
    }
}
