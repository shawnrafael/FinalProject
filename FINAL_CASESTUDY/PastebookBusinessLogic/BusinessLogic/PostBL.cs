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
                        if (listOFUsers.Any(x=> (x.ID == post.POSTER_ID && x.ID == post.PROFILE_OWNER_ID) || userID == post.POSTER_ID))
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

        public List<POST> RetrieveListOfPostOnProfile(string username)
        {
            var listOfPost = new List<POST>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    listOfPost = context.POSTs
                                .Include("USER")
                                .Include("LIKEs")
                                .Where(x => x.USER.USER_NAME == username)
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

        public int LikeSpecificPost(int userID, int postID)
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
                    context.LIKEs.Add(like);
                    status = context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return status;
        }
    }
}
