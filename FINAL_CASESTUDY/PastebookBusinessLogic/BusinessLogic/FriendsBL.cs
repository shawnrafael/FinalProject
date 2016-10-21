using DataAccess.AccessLayer;
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
        GenericDataAccess<FRIEND> accessFriend = new GenericDataAccess<FRIEND>();
        GenericDataAccess<USER> accessUser = new GenericDataAccess<USER>();
        PasteBookAccessLayer pasteBookAL = new PasteBookAccessLayer();

        public List<USER> RetrieveFriends(int userID)
        {
            var friendList = pasteBookAL.RetrieveListOfFriends(userID);
            return friendList;
        }

        public bool CheckIfRequestSent(int userID, int currentUserProfile)
        {
            bool exist = pasteBookAL.CheckRequest(userID, currentUserProfile);
            return exist;
        }

        public bool CheckFriendUser(int userID, int currentUser)
        {
            bool checkUser = pasteBookAL.CheckFriendUser(userID, currentUser);
            return checkUser;
        }

        public bool CheckIfFriends(int userID, int currentUserProfile)
        {
            var listOfFriends = accessFriend.EntityList();

            bool friends = listOfFriends.Where(x => x.REQUEST == "N").Any(x => (x.USER_ID == currentUserProfile && x.FRIEND_ID == userID) || (x.USER_ID == userID && x.FRIEND_ID == currentUserProfile));

            return friends;
        }
       

        public bool AddFriendRequest(int userID, int currentUserProfile)
        {
            
            var newFriend = new FRIEND()
            {
                FRIEND_ID = currentUserProfile,
                USER_ID = userID,
                REQUEST = "Y",
                BLOCKED = "N",
                CREATED_DATE = DateTime.Now
            };

            bool request = accessFriend.Create(newFriend);
            return request;
        }
    }
}
