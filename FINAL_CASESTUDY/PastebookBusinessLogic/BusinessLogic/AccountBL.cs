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

namespace PastebookBusinessLogic.BusinessLogic
{    
    public class AccountBL
    {
        PasteBookAccessLayer pasteBookAL = new PasteBookAccessLayer();
        GenericDataAccess<USER> accessUser = new GenericDataAccess<USER>();
        GenericDataAccess<REF_COUNTRY> accessCountry = new GenericDataAccess<REF_COUNTRY>();
        PasswordBL passwordBL = new PasswordBL();

        public bool CheckUserName(USER newUser)
        {
            var user = pasteBookAL.RetrieveUser(newUser.USER_NAME);
            if (user.ID != 0)
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
            var user = pasteBookAL.RetrieveLoginUser(email);

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

        public List<REF_COUNTRY> GetCountryList()
        {
            List<REF_COUNTRY> listOfCOuntry = accessCountry.EntityList().ToList();
            return listOfCOuntry;
        }
    }
}
