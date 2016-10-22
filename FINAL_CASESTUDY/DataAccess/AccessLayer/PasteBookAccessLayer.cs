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

        public List<POST> RetrieveListOfPost()
        {
            var listOfPost = new List<POST>();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    listOfPost = context.POSTs
                                        .Include("USER")
                                        .Include("COMMENTs")
                                        .Include("LIKEs")
                                        .OrderByDescending(x => x.CREATED_DATE)
                                        .ToList();
                }
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {

                throw;
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
                                                .Where(x => x.RECEIVER_ID == userID && x.SEEN == "N")
                                                .ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return notifications;
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
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {

                throw;
            }
            return likers;
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
                                  .Where(x => x.ID == postID).Single();
                }
            }
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
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
                        user = context.USERs.Where(x => x.USER_NAME == username).Single();
                    }
                }
            }
            catch (Exception)
            {

                throw;
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
                        user = context.USERs.Where(x => x.ID == userID).Single();
                    }
                }
            }
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
            }
            return user;
        }       

    }
}
