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
        public List<VWUserProfilePost> RetrieveListOfPostOnProfile(int userID)
        {
            var listOfPost = new List<VWUserProfilePost>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    listOfPost = context.VWUserProfilePosts
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

        public USER PostOnOwnProfile(string userID)
        {
            var user = new USER();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    user = context.USERs.Where(x => x.USER_NAME == userID).SingleOrDefault();
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
    }
}
