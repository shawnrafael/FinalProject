using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.AccessLayer
{
    public class PasteBookAccessLayer
    {
        List<Exception> errorList = new List<Exception>();

        public List<POST> RetrieveListOfPost()
        {
            var listOfPost = new List<POST>();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    listOfPost = context.POSTs
                                        .Include("USER1")
                                        .Include("USER")
                                        .Include("COMMENTs")
                                        .Include("LIKEs")
                                        .OrderByDescending(x => x.CREATED_DATE)
                                        .ToList();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }

            return listOfPost;
        }

        public List<COMMENT> RetrieveListOfComment(int postID)
        {
            var listOfComments = new List<COMMENT>();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    listOfComments = context.COMMENTs
                                        .Include("USER")
                                        .Where(x=>x.POST_ID == postID)
                                        .OrderBy(x => x.DATE_CREATED)
                                        .ToList();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }

            return listOfComments;
        }        

        public List<LIKE> RetrieveListOfLike(int currentPost)
        {
            var listOfLikes = new List<LIKE>();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    listOfLikes = context.LIKEs
                                        .Include("USER")
                                        .Where(x => x.POST_ID == currentPost)
                                        .ToList();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }

            return listOfLikes;
        }

        public List<FRIEND> RetrieveFriends(int userID)
        {
            var friends = new List<FRIEND>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    friends = context.FRIENDs.Where(x => x.USER_ID == userID || x.FRIEND_ID == userID).ToList();
                }
            }
            catch (Exception ex)
            {

                errorList.Add(ex);
            }
            return friends;
        }

        public List<NOTIFICATION> RetrieveNotifications(int userID)
        {
            var notifications = new List<NOTIFICATION>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    notifications = context.NOTIFICATIONs
                                           .Include("USER")
                                           .Where(x => x.RECEIVER_ID == userID && x.NOTIF_TYPE!="F")
                                           .OrderByDescending(x=>x.CREATED_DATE)
                                           .ToList();
                }
            }
            catch (Exception ex)
            {

                errorList.Add(ex);
            }
            return notifications;
        }

        public List<NOTIFICATION> RetrieveRequests(int userID)
        {
            var requests = new List<NOTIFICATION>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    requests = context.NOTIFICATIONs
                                           .Include("USER")
                                           .Where(x => x.RECEIVER_ID == userID && x.NOTIF_TYPE == "F" && x.SEEN=="N")
                                           .OrderByDescending(x => x.CREATED_DATE)
                                           .ToList();
                }
            }
            catch (Exception ex)
            {

                errorList.Add(ex);
            }
            return requests;
        }

        public List<USER> RetrieveSearch(string keyWord)
        {
            var results = new List<USER>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    results = context.USERs.Where(x => x.FIRST_NAME.Contains(keyWord) || x.LAST_NAME.Contains(keyWord) || ((x.FIRST_NAME+x.LAST_NAME).Contains(keyWord)) || (x.FIRST_NAME+" "+x.LAST_NAME)==keyWord ).ToList();

                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }

            return results;

        }

        public List<USER> RetrieveListOfFriends(int userID)
        {
            var friends = new List<USER>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var result = context.FRIENDs.Where(x => (x.FRIEND_ID == userID || x.USER_ID == userID) && x.REQUEST == "N");

                    foreach (var item in result)
                    {
                        if (item.USER_ID == userID)
                        {
                            friends.Add(context.USERs.Where(x => x.ID == item.FRIEND_ID).Single());
                        }
                        else
                        {
                            friends.Add(context.USERs.Where(x => x.ID == item.USER_ID).Single());
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }

            return friends;

        }

        public List<USER> RetrieveListOfLikers(int currentPost)
        {
            var likers = new List<USER>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var results = context.LIKEs.Where(x => x.POST_ID == currentPost).ToList();

                    foreach (var item in results)
                    {
                        likers.Add(context.USERs.Where(x => x.ID == item.LIKED_BY).Single());
                    }
                }
            }
            catch (Exception ex)
            {

                errorList.Add(ex);
            }
            return likers;
        }

        public FRIEND RetrieveFriend(int userID, int currentProfileID)
        {
            var friend = new FRIEND();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    friend = context.FRIENDs.Where(x => (x.USER_ID == userID && x.FRIEND_ID == currentProfileID) || (x.USER_ID == currentProfileID && x.FRIEND_ID == userID)).Single();
                }
            }
            catch (Exception ex)
            {

                errorList.Add(ex);
            }
            return friend;
        }

        public NOTIFICATION RetrieveNotification(int notifID)
        {
            var notification = new NOTIFICATION();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    notification = context.NOTIFICATIONs.Where(x => x.ID == notifID).Single();
                }
            }
            catch (Exception ex)
            {

                errorList.Add(ex);
            }
            return notification;
        }

        public NOTIFICATION RetrieveNotificationRequest(int userID, int senderID)
        {
            var notification = new NOTIFICATION();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    notification = context.NOTIFICATIONs.Where(x => x.SENDER_ID == senderID && x.RECEIVER_ID == userID && x.NOTIF_TYPE == "F").Single();
                }
            }
            catch (Exception ex)
            {

                errorList.Add(ex);
            }
            return notification;
        }

        public POST RetrienvePost(int postID)
        {
            var post = new POST();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    post = context.POSTs
                                  .Include("COMMENTs")
                                  .Include("LIKEs")
                                  .Include("USER")
                                  .Include("USER1")                                  
                                  .Where(x => x.ID == postID).Single();
                }
            }
            catch (Exception ex)
            {

                errorList.Add(ex);
            }
            return post;
        }

        public COMMENT RetrieveComment(COMMENT comment)
        {
            var result = new COMMENT();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    result = context.COMMENTs.Where(x => x.POSTER_ID == comment.POSTER_ID && x.POST_ID == comment.POST_ID && x.DATE_CREATED == comment.DATE_CREATED).Single();
                }
            }
            catch (Exception ex)
            {

                errorList.Add(ex);
            }
            return result;
        }

        public LIKE RetrieveLike(LIKE like)
        {
            var result = new LIKE();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    result = context.LIKEs.Where(x => x.LIKED_BY == like.LIKED_BY && x.POST_ID == like.POST_ID).Single();
                }
            }
            catch (Exception ex)
            {

                errorList.Add(ex);
            }
            return result;
        }

        public USER RetrieveUser(string username)
        {
            var user = new USER();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    if (context.USERs.Any(x=>x.USER_NAME == username))
                    {
                        user = context.USERs.Include("FRIENDs").Include("REF_COUNTRY").Where(x => x.USER_NAME == username).Single();
                    }
                }
            }
            catch (Exception ex)
            {

                errorList.Add(ex);
            }
            return user;
        }

        public USER RetrieveUser(int userID)
        {
            var user = new USER();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    if (context.USERs.Any(x => x.ID == userID))
                    {
                        user = context.USERs.Include("FRIENDs").Include("FRIENDs1").Where(x => x.ID == userID).Single();
                    }
                }
            }
            catch (Exception ex)
            {

                errorList.Add(ex);
            }
            return user;
        }

        public USER RetrieveLoginUser(string email)
        {
            var user = new USER();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    if (context.USERs.Any(x => x.EMAIL_ADDRESS == email))
                    {
                        user = context.USERs.Where(x => x.EMAIL_ADDRESS == email).Single();
                    }
                }
            }
            catch (Exception ex)
            {

                errorList.Add(ex);
            }
            return user;
        }       

    }
}
