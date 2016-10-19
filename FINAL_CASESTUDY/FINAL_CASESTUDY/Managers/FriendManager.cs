using FINAL_CASESTUDY.Models;
using PastebookBusinessLogic.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FINAL_CASESTUDY.Managers
{    
    public class FriendManager
    {
        FriendsBL friendsBL = new FriendsBL();

        public List<User> GetFriends(int userID)
        {
            List<User> listOfFriends = new List<User>();

            var friends = friendsBL.RetrieveFriends(userID);

            foreach (var item in friends)
            {
                listOfFriends.Add(Mapper.ToUser(item));
            }
            return listOfFriends;
        }
    }
}