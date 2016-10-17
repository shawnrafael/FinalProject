using FINAL_CASESTUDY.Models;
using PastebookBusinessLogic.Entities;
using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FINAL_CASESTUDY.Managers
{
    public static class Mapper
    {
        public static USER ToUSER(User user)
        {
            return new USER()
            {
                USER_NAME = user.Username,
                PASSWORD = user.Password,
                EMAIL_ADDRESS = user.Email,
                FIRST_NAME = user.FirstName,
                LAST_NAME = user.LastName,
                BIRTHDAY = user.Birthday,
                COUNTRY_ID = user.CountryID,
                MOBILE_NO = user.MobileNumber,
                GENDER = user.Gender,
                PROFILE_PIC = user.ProfilePicture,
                ABOUT_ME = user.AboutMe
            };
        }
        public static Country ToCountryList(CountryEntity country)
        {
            return new Country()
            {
                CountryID = country.CountryID,
                CountryName = country.Country
            };
        }
    }
}