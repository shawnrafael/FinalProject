using DataAccess.PasteBookAccessLayer;
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
        DataAccess<COMMENT> accessComment = new DataAccess<COMMENT>();

        public bool AddComment(COMMENT newComment)
        {
            bool status = accessComment.Create(newComment);
            return status;
        }
    }
}
