using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic.BusinessLogic
{
    public class FriendsBL
    {
        public List<USER> RetrieveFriends(int userID)
        {
            var listOfFriends = new List<USER>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var friends = context.FRIENDs
                                  .Where(x => x.USER_ID == userID || x.FRIEND_ID == userID).ToList();

                    foreach (var item in friends)
                    {
                        if (item.USER_ID == userID)
                        {
                            listOfFriends.Add(context.USERs.Where(x => x.ID == item.FRIEND_ID).First());
                        }
                        else
                        {
                            listOfFriends.Add(context.USERs.Where(x => x.ID == item.USER_ID).First());
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return listOfFriends;
        }
    }
}
