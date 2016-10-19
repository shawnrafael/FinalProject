using FINAL_CASESTUDY.Models;
using PastebookBusinessLogic.BusinessLogic;
using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FINAL_CASESTUDY.Managers
{    
    public class AccountManager
    {
        AccountBL accountBL = new AccountBL();

        public bool RegisterUser(User user)
        {
            if (user.Gender == null)
            {
                user.Gender = "U";
            }

            int result = accountBL.Register(Mapper.ToUSERFromDB(user));
            return result != 0;
        }

        public List<Country> GetCountryList()
        {
            List<Country> listCountry = new List<Country>();
            foreach (var item in accountBL.GetCountryList())
            {
                listCountry.Add(Mapper.ToCountryList(item));
            }
            return listCountry;
        }

        public User LoginUser(string email, string password)
        {
            var user = new User();
            USER retrievedUser = accountBL.LoginUser(email, password);
            if (retrievedUser != null)
            {
                user = Mapper.ToUser(retrievedUser);
            }            
            return user;
        }

        public User GetUserByID(int userID)
        {
            var user = Mapper.ToUser(accountBL.GetUserByID(userID));
            return user;
        }
        public User GetUserByUsername(string username)
        {
            var user = Mapper.ToUser(accountBL.GetUserByUsername(username));
            return user;
        }
    }
}