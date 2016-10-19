using PastebookBusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteBookEntity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace PastebookBusinessLogic.BusinessLogic
{
    public class AccountBL
    {
        PasswordBL passwordBL = new PasswordBL();

        public int Register(USER newUser)
        {
            int status = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    string salt = null;
                    string hash = passwordBL.GeneratePasswordHash(newUser.PASSWORD, out salt);
                    newUser.SALT = salt;
                    newUser.PASSWORD = hash;
                    newUser.DATE_CREATED = DateTime.Now;

                    context.USERs.Add(newUser);
                    status = context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return status;
        }

        public USER LoginUser(string email, string password)
        {
            USER user = new USER();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    user = context.USERs.Where(x => x.EMAIL_ADDRESS == email).SingleOrDefault();
                    if (user == null)
                    {
                        return user;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            var match = passwordBL.IsPasswordMatch(password, user.SALT, user.PASSWORD);
            if (match == true)
            {
                return user;
            }
            return user = null;
        }        

        public USER GetUserByID(int userID)
        {
            USER user = new USER();
            try
            {
                using (var contex = new PASTEBOOKEntities())
                {
                    user = contex.USERs.Where(x => x.ID == userID).SingleOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return user;
        }

        public USER GetUserByUsername(string username)
        {
            USER user = new USER();
            try
            {
                using (var contex = new PASTEBOOKEntities())
                {
                    user = contex.USERs.Where(x => x.USER_NAME == username).SingleOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return user;
        }
        public List<CountryEntity> GetCountryList()
        {
            var countryList = new List<CountryEntity>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    foreach (var item in context.REF_COUNTRY)
                    {
                        countryList.Add(new CountryEntity() { CountryID = item.ID, Country = item.COUNTRY });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return countryList;
        }

    }
}
