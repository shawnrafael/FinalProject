using DataAccess.PasteBookAccessLayer;
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
        DataAccess<FRIEND> accessFriend = new DataAccess<FRIEND>();
        DataAccess<USER> accessUser = new DataAccess<USER>();

        public List<USER> RetrieveFriends(int userID)
        {
            var listOfFriends = accessFriend.EntityList();
            var listOfUsers = accessUser.EntityList();

            var friendList = new List<USER>();

            foreach (var item in listOfFriends)
            {
                if (item.USER_ID == userID)
                {
                    friendList.Add(listOfUsers.Where(x => x.ID == item.FRIEND_ID).Single());
                }
                else
                {
                    friendList.Add(listOfUsers.Where(x => x.ID == item.USER_ID).Single());
                }
            }
            
            return friendList;
        }
    }
}
