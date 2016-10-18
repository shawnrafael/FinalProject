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

    public class PasteBookBL
    {
        public int Register(USER newUser)
        {
            int status = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    string salt = null;
                    string hash = GeneratePasswordHash(newUser.PASSWORD, out salt);
                    newUser.SALT = salt;
                    newUser.PASSWORD = hash;
                    newUser.DATE_CREATED = DateTime.Now;

                    context.USERs.Add(newUser);
                    status = context.SaveChanges();
                }
            }
            catch(Exception)
            {
                throw;
            }
            return status;
        }

        public string GeneratePasswordHash(string password, out string salt)
        {
            SaltGenerator saltGen = new SaltGenerator();
            HashComputer m_hashComputer = new HashComputer();
            salt = saltGen.GetSaltString();

            string finalString = password + salt;

            return m_hashComputer.GetPasswordHashAndSalt(finalString);
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

        public int LoginUser( string email, string password )
        {
            int status = 0;
            USER user = new USER();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    user = context.USERs.Where(x => x.EMAIL_ADDRESS == email).SingleOrDefault();
                    if (user == null)
                    {
                        return status;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            var match = IsPasswordMatch(password, user.SALT, user.PASSWORD);
            if(match == true)
            {
                status = 1;
            }
            return status;
        }

        public bool IsPasswordMatch(string password, string salt, string hash)
        {
            HashComputer m_hashComputer = new HashComputer();
            string finalString = password + salt;
            return hash == m_hashComputer.GetPasswordHashAndSalt(finalString);
        }

        public USER GetLoginUser(string email)
        {
            USER user = new USER();
            try
            {
                using (var contex = new PASTEBOOKEntities())
                {
                    user = contex.USERs.Where(x => x.EMAIL_ADDRESS == email).SingleOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return user;
        }

        public USER GetUser(string loginUser)
        {
            USER user = new USER();
            try
            {
                using (var contex = new PASTEBOOKEntities())
                {
                    user = contex.USERs.Where(x => x.USER_NAME == loginUser).SingleOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return user;
        }
    }
}