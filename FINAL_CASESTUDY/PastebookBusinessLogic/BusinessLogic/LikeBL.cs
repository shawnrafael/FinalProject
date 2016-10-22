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
        PasteBookAccessLayer pasteBookAL = new PasteBookAccessLayer();

        public LIKE LikePost(int userID, int currentPost)
        {
            var listOfLike = pasteBookAL.RetrieveListOfLike(currentPost);

            LIKE like = new LIKE()
            {
                LIKED_BY = userID,
                POST_ID = currentPost
            };

            bool status = false;

            if (listOfLike.Any(x=>(x.LIKED_BY == like.LIKED_BY)) == false)
            {
                status = accessLike.Create(like);
            }

            return like;
        }

        public List<USER> DisplayLikers(int postID)
        {
            var listOfLikers = pasteBookAL.RetrieveListOfLikers(postID);
            return listOfLikers;
        }

    }
}
