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
        PasteBookAccessLayer pasteBookAL = new PasteBookAccessLayer();

        public List<USER> RetrieveFriends(int userID)
        {
            var friendList = pasteBookAL.RetrieveListOfFriends(userID);
            return friendList;
        }

        public string CheckFriendRequest(int userID, int currentUserProfile)
        {
            var friends = pasteBookAL.RetrieveFriends(userID);
            string status = "no request";

            if (friends.Any(x=>x.USER_ID == userID && x.FRIEND_ID == currentUserProfile && x.REQUEST == "Y"))
            {
                status = "user request";
            }
            else if (friends.Any(x=>x.USER_ID == currentUserProfile && x.FRIEND_ID == userID && x.REQUEST == "Y"))
            {
                status = "friend confirm";
            }
            else if (friends.Any(x=>((x.USER_ID == currentUserProfile && x.FRIEND_ID == userID) || (x.USER_ID == userID && x.FRIEND_ID == currentUserProfile)) && x.REQUEST == "N"))
            {
                status = "friends";
            }
            else if (userID == currentUserProfile)
            {
                status = "profile owner";
            }

            return status;
        }

        public bool AddFriend(int userID, int currentUserProfile)
        {
            var friends = pasteBookAL.RetrieveFriends(userID);
            var newRequest = new FRIEND()
            {
                USER_ID = userID,
                FRIEND_ID = currentUserProfile,
                CREATED_DATE = DateTime.Now,
                REQUEST = "Y",
                BLOCKED = "N"
            };

            bool added = false;
            if (friends.Count == 0)
            {
                added = accessFriend.Create(newRequest);
            }
            else if (friends.Any(x=>x.USER_ID == userID && x.FRIEND_ID == currentUserProfile)==false)
            {
                added = accessFriend.Create(newRequest);
            }            
            return added;
        }

        public FRIEND GetFriendRequest(int userID, int currentUserProfile)
        {
            var friend = pasteBookAL.RetrieveFriend(userID, currentUserProfile);
            if (friend.ID != 0)
            {
                return friend; 
            }
            return friend;
        }

        public bool ConfirmFriend(int userID, int currentUserProfile)
        {
            bool confirm = false;
            var requests = pasteBookAL.RetrieveFriends(userID);
            var id = requests.Where(x => x.USER_ID == currentUserProfile && x.FRIEND_ID == userID).Select(x=>x.ID).Single();

            var updateRequest = new FRIEND()
            {
                ID = id,
                USER_ID = currentUserProfile,
                FRIEND_ID = userID,
                CREATED_DATE = DateTime.Now,
                REQUEST = "N",
                BLOCKED = "N"
            };
            confirm = accessFriend.Edit(updateRequest);

            return confirm;
        }

        public bool DeleteFriendRequest(int userID, int currentUserProfile)
        {
            bool delete = false;
            var requests = pasteBookAL.RetrieveFriends(userID);
            var id = requests.Where(x => x.USER_ID == currentUserProfile && x.FRIEND_ID == userID).Select(x => x.ID).Single();

            var updateRequest = new FRIEND()
            {
                ID = id,
                USER_ID = currentUserProfile,
                FRIEND_ID = userID,
                CREATED_DATE = DateTime.Now,
                REQUEST = "N",
                BLOCKED = "N"
            };
            delete = accessFriend.Delete(updateRequest);

            return delete;
        }

        public List<USER> SearchFriend(string keyWord)
        {
            var result = pasteBookAL.RetrieveSearch(keyWord);
            return result;
        }

    }
}
