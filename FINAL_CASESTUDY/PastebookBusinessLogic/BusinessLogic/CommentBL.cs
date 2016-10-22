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
        PasteBookAccessLayer pasteBookAL = new PasteBookAccessLayer();

        public COMMENT AddComment(string commentContent,int userID,int currentPost)
        {
            var newComment = new COMMENT()
            {
                CONTENT = commentContent,
                POSTER_ID = userID,
                POST_ID = currentPost,
                DATE_CREATED = DateTime.Now
            };

            bool status = accessComment.Create(newComment);

            return newComment;
        }

        public List<COMMENT> RetrieveComments(int postID)
        {
            var comments = pasteBookAL.RetrieveListOfComment(postID);
            return comments;
        }
    }
}
