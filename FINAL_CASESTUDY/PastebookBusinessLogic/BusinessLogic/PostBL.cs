using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic.BusinessLogic
{
    public class PostBL
    {      
        public List<POST> RetrieveListOfPost(List<USER> listOFUsers, int userID)
        {
            var listOfPost = new List<POST>();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var result = context.POSTs
                                    .Include("COMMENTs")
                                    .Include("LIKEs")
                                    .Include("USER")
                                    .OrderByDescending(x=>x.CREATED_DATE)
                                    .ToList();
                    foreach (var post in result)
                    {
                        if (listOFUsers.Any(x=> (x.ID == post.POSTER_ID) || userID == post.POSTER_ID))
                        {
                            listOfPost.Add(post);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return listOfPost;

        }

        public List<POST> RetrieveListOfPostOnProfile(int userID)
        {
            var listOfPost = new List<POST>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    listOfPost = context.POSTs
                                .Include("USER")
                                .Include("LIKEs")
                                .Include("COMMENTs")
                                .Where(x => x.PROFILE_OWNER_ID == userID)
                                .OrderByDescending(x => x.CREATED_DATE)
                                .Select(x => x).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listOfPost;
        }

        public USER PostOnOwnProfile(int userID)
        {
            var user = new USER();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    user = context.USERs.Where(x => x.ID == userID).SingleOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return user;
        }

        public int AddPost(POST post)
        {
            int status = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    post.CREATED_DATE = DateTime.Now;
                    context.POSTs.Add(post);
                    status = context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return status;
        }

        public int UserLikePost(int userID, int postID)
        {
            int status = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var like = new LIKE()
                    {
                        POST_ID = postID,
                        LIKED_BY = userID
                    };

                    if (context.LIKEs.Any(x=> (x.LIKED_BY == like.LIKED_BY && x.POST_ID == like.POST_ID)))
                    {
                        return status;
                    }
                    else
                    {
                        context.LIKEs.Add(like);
                        status = context.SaveChanges();
                    }
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
            return status;
        }

        public int UserCommentPost(int userID, int postID, string commentContent)
        {
            int status = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var comment = new COMMENT()
                    {
                        POST_ID = postID,
                        POSTER_ID = userID,
                        CONTENT = commentContent,
                        DATE_CREATED = DateTime.Now
                    };

                    context.COMMENTs.Add(comment);
                    status = context.SaveChanges();


                }
            }
            catch (Exception)
            {

                throw;
            }
            return status;
        }
        //public List<USER> DisplayLikers(int postID)
        //{
        //    var listOfLikers = new List<USER>();
        //    var listOfUserID = ListOfLikes(postID);

        //    try
        //    {
        //        using (var context = new PASTEBOOKEntities())
        //        {

        //            var results = context.USERs.Include("POSTs").Include("LIKEs").ToList();
        //            foreach (var liker in results)
        //            {
        //                if (results.Any(x => (x.ID == postID) && (x.USER.ID == )))
        //                {
        //                    listOfLikers.Add(liker.);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return listOfLikers;
        //}

        public List<USER> DisplayLikers(int postID)
        {
            var listOfLikers = new List<USER>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var results = context.LIKEs.Include("USER").Where(x=>x.POST_ID==postID);
                    foreach (var item in results)
                    {
                        listOfLikers.Add(item.USER);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listOfLikers;
        }
    }
}
