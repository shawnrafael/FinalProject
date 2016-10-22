using DataAccess.AccessLayer;
using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic.BusinessLogic
{
    public class NotificationBL
    {
        GenericDataAccess<NOTIFICATION> accessNotify = new GenericDataAccess<NOTIFICATION>();
        PasteBookAccessLayer pasteBookAL = new PasteBookAccessLayer();

        public bool AddNotification(LIKE like)
        {
            var post = pasteBookAL.RetrienvePost(like.POST_ID);
            bool notify = false;

            var newNotification = new NOTIFICATION()
            {
                RECEIVER_ID = post.POSTER_ID,
                NOTIF_TYPE = "L",
                SENDER_ID = like.LIKED_BY,
                CREATED_DATE = DateTime.Now,
                POST_ID = like.POST_ID,
                SEEN = "N"
            };
            if (like.LIKED_BY != post.POSTER_ID)
            {
                notify = accessNotify.Create(newNotification);
            }
            return notify;
        }

        public bool AddNotification(COMMENT comment)
        {
            var post = pasteBookAL.RetrienvePost(comment.POST_ID);
            bool notify = false;

            var newNotification = new NOTIFICATION()
            {
                RECEIVER_ID = post.POSTER_ID,
                NOTIF_TYPE = "C",
                SENDER_ID = comment.POSTER_ID,
                CREATED_DATE = DateTime.Now,
                COMMENT_ID = comment.ID,
                POST_ID = comment.POST_ID,
                SEEN = "N"
            };
            
            if (comment.POSTER_ID != post.PROFILE_OWNER_ID)
            {
                notify = accessNotify.Create(newNotification);
            }

            return notify;           
        }

        public void AddNotification(FRIEND like)
        {

        }

        public List<NOTIFICATION> RetrieveNotifications(int userID)
        {
            var notifications = pasteBookAL.RetrieveNotifications(userID);
            return notifications;
        }
    }
}
