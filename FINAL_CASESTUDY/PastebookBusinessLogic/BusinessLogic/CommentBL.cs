using DataAccess.AccessLayer;
using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic.BusinessLogic
{
    public class CommentBL
    {
        GenericDataAccess<COMMENT> accessComment = new GenericDataAccess<COMMENT>();
        PasteBookAccessLayer pasteBoolAL = new PasteBookAccessLayer();

        public bool AddComment(string commentContent,int userID,int currentPost)
        {
            var newComment = new COMMENT()
            {
                CONTENT = commentContent,
                POSTER_ID = userID,
                POST_ID = currentPost,
                DATE_CREATED = DateTime.Now
            };
            bool status = accessComment.Create(newComment);
            return status;
        }

        public List<COMMENT> RetrieveComments(int postID)
        {
            var listOfComments = pasteBoolAL.RetrieveListOfComment();
            var comments = listOfComments.OrderBy(x=>x.DATE_CREATED).Where(x => x.POST_ID == postID).ToList();

            return comments;
        }
    }
}
