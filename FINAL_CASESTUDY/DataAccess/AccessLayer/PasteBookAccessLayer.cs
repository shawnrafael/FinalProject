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

        public List<COMMENT> RetrieveListOfComment()
        {
            var listOfComments = new List<COMMENT>();

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    listOfComments = context.COMMENTs
                                        .Include("USER")
                                        .OrderByDescending(x => x.DATE_CREATED)
                                        .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return listOfComments;
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

        public bool CheckRequest(int userID, int currentUserID)
        {
            bool exist = false;

            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    exist = context.FRIENDs
                                    .Where(x => x.USER_ID == userID || x.FRIEND_ID == userID)
                                    .Any(x => x.FRIEND_ID == currentUserID || x.USER_ID == currentUserID);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return exist;
        }

        public bool CheckFriendUser(int userID, int currentUserID)
        {
            bool checkUser = false;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    if (context.FRIENDs.Any(x => x.USER_ID == userID && x.FRIEND_ID == currentUserID))
                    {
                        checkUser = true;
                    }
                    else
                    {
                        checkUser = false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return checkUser;
        }
    }
}
