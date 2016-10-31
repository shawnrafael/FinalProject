using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteBookEntity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using DataAccess.AccessLayer;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

namespace PastebookBusinessLogic.BusinessLogic
{    
    public class AccountBL
    {
        PasteBookAccessLayer pasteBookAL = new PasteBookAccessLayer();
        GenericDataAccess<USER> accessUser = new GenericDataAccess<USER>();
        GenericDataAccess<REF_COUNTRY> accessCountry = new GenericDataAccess<REF_COUNTRY>();
        PasswordBL passwordBL = new PasswordBL();

        public bool Register(USER newUser)
        {
            newUser.PASSWORD = Regex.Replace(newUser.PASSWORD, @"&lt", "<");
            newUser.PASSWORD = Regex.Replace(newUser.PASSWORD, @"&gt", ">");

            newUser.FIRST_NAME = Regex.Replace(newUser.FIRST_NAME, @"\s+", " ");
            newUser.FIRST_NAME = newUser.FIRST_NAME.Trim();

            newUser.LAST_NAME = Regex.Replace(newUser.LAST_NAME, @"\s+", " ");
            newUser.LAST_NAME = newUser.LAST_NAME.Trim();

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
            var user = pasteBookAL.RetrieveLoginUser(email);
            password = Regex.Replace(password, @"&lt", "<");
            password = Regex.Replace(password, @"&gt", ">");

            bool match = passwordBL.IsPasswordMatch(password, user.SALT, user.PASSWORD);
            if (match == true)
            {
                return user;
            }
            return user = null;
            
        }        

        public USER GetUserByID(int userID)
        {
            var user = pasteBookAL.RetrieveUser(userID);

            return user;
        }

        public USER GetUserByUsername(string username)
        {
            var user = pasteBookAL.RetrieveUser(username);

            return user;
        }

        public bool UploadProfilePic(HttpPostedFileBase image, USER user)
        {
            byte[] profilePic = null;
            using (MemoryStream ms = new MemoryStream())
            {
                image.InputStream.CopyTo(ms);
                profilePic = ms.GetBuffer();
            }
            user.PROFILE_PIC = profilePic;
            return accessUser.Edit(user);
        }

        public bool UpdateAboutMe(string aboutMe, USER user)
        {            
            user.ABOUT_ME = aboutMe;
            return accessUser.Edit(user);
        }

        public bool UpdateProfileInfo(USER user)
        {
            user.FIRST_NAME = Regex.Replace(user.FIRST_NAME, @"\s+", " ");
            user.FIRST_NAME = user.FIRST_NAME.Trim();

            user.LAST_NAME = Regex.Replace(user.LAST_NAME, @"\s+", " ");
            user.LAST_NAME = user.LAST_NAME.Trim();

            if (user.GENDER == null)
            {
                user.GENDER = "U";
            }

            return accessUser.Edit(user);
        }

        public bool CheckPassword(string oldPassWord, int userID)
        {
            var user = pasteBookAL.RetrieveUser(userID);
            bool match = passwordBL.IsPasswordMatch(oldPassWord, user.SALT, user.PASSWORD);
            if (match == true)
            {
                return true;
            }
            return false;
        }

        public bool CheckEmail(string email)
        {
            var user = pasteBookAL.RetrieveLoginUser(email);            
            if (user.ID != 0)
            {
                return true;
            }
            return false;
        }

        public bool CheckUsername(string username)
        {
            var user = pasteBookAL.RetrieveUser(username);
            if (user.ID != 0)
            {
                return true;
            }
            return false;
        }

        public bool UpdatePassword(string newPassword, int userID)
        {
            var user = pasteBookAL.RetrieveUser(userID);
            string salt = null;
            string hash = passwordBL.GeneratePasswordHash(newPassword, out salt);

            user.SALT = salt;
            user.PASSWORD = hash;

            bool successUpdate = accessUser.Edit(user);
            return successUpdate;
        }

        public bool UpdateEmail(string newEmail, int userID)
        {
            var user = pasteBookAL.RetrieveUser(userID);
            user.EMAIL_ADDRESS = newEmail;
            bool successUpdate = accessUser.Edit(user);
            return successUpdate;
        }

        public List<REF_COUNTRY> GetCountryList()
        {
            List<REF_COUNTRY> listOfCOuntry = accessCountry.EntityList().ToList();
            return listOfCOuntry;
        }
    }
}
