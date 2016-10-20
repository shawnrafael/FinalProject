using PastebookBusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteBookEntity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using DataAccess.PasteBookAccessLayer;

namespace PastebookBusinessLogic.BusinessLogic
{    
    public class AccountBL
    {
        DataAccess<USER> accessUser = new DataAccess<USER>();
        DataAccess<REF_COUNTRY> accessCountry = new DataAccess<REF_COUNTRY>();
        PasswordBL passwordBL = new PasswordBL();

        public bool CheckUser(USER newUser)
        {
            var listOfUsers = accessUser.EntityList();
            if (listOfUsers.Any(x => x.USER_NAME == newUser.USER_NAME))
            {
                return false;
            }
            return true;
        }

        public bool Register(USER newUser)
        {            
            if (newUser.GENDER == null)
            {
                newUser.GENDER = "U";
            }

            string salt = null;
            string hash = passwordBL.GeneratePasswordHash(newUser.PASSWORD, out salt);
            
            newUser.SALT = salt;
            newUser.PASSWORD = hash;
            newUser.DATE_CREATED = DateTime.Now;

            

            bool successRegister = accessUser.Create(newUser);
            return successRegister;
        }

        public USER LoginUser(string email, string password)
        {
            List<USER> listOfUser = accessUser.EntityList();
            USER user = new USER();

            if (listOfUser.Any(x => x.EMAIL_ADDRESS == email))
            {
                user = listOfUser.Where(x => x.EMAIL_ADDRESS == email).Single();                
            }

            bool match = passwordBL.IsPasswordMatch(password, user.SALT, user.PASSWORD);
            if (match == true)
            {
                return user;
            }
            return user = null;
            
        }        

        public USER GetUserByID(int userID)
        {
            var users = accessUser.EntityList();
            var user = users.Where(x => x.ID == userID).Single(); 

            return user;
        }

        public USER GetUserByUsername(string username)
        {
            var users = accessUser.EntityList();
            var user = users.Where(x => x.USER_NAME == username).Single();
            return user;
        }

        public List<REF_COUNTRY> GetCountryList()
        {
            List<REF_COUNTRY> listOfCOuntry = accessCountry.EntityList().ToList();
            return listOfCOuntry;
        }
    }
}
