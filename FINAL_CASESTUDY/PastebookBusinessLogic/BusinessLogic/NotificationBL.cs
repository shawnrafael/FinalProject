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
            
            if (comment.POSTER_ID != post.POSTER_ID)
            {
                notify = accessNotify.Create(newNotification);
            }

            return notify;           
        }

        public bool AddNotification(int userID, int friendID)
        {
            var newNotification = new NOTIFICATION()
            {
                RECEIVER_ID = friendID,
                NOTIF_TYPE = "F",
                SENDER_ID = userID,
                CREATED_DATE = DateTime.Now,
                SEEN = "N"
            };
            bool notify = accessNotify.Create(newNotification);
            return notify;

        }

        public List<NOTIFICATION> RetrieveNotifications(int userID)
        {
            var notifications = pasteBookAL.RetrieveNotifications(userID);
            return notifications;
        }

        public List<NOTIFICATION> RetrieveRequests(int userID)
        {
            var notifications = pasteBookAL.RetrieveRequests(userID);
            return notifications;
        }

        public NOTIFICATION RetrieveFriendRequest(int userID, int senderID)
        {
            var notifications = pasteBookAL.RetrieveNotificationRequest(userID, senderID);
            return notifications;
        }

        public bool UpdateSeen(NOTIFICATION notif)
        {
            bool seen = false;
            notif.SEEN = "Y";

            seen = accessNotify.Edit(notif);
            return seen;

        }

        public bool DeleteNotification(NOTIFICATION notif)
        {
            bool delete = accessNotify.Delete(notif);
            return delete;
        }
    }
}
