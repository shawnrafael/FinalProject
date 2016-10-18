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
        public static USER ToUSERFromDB(User user)
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
        public static User ToUser(USER user)
        {
            return new User()
            {
                UserID = user.ID,
                Username = user.USER_NAME,
                Password = user.PASSWORD,
                Email = user.EMAIL_ADDRESS,
                FirstName = user.FIRST_NAME,
                LastName = user.LAST_NAME,
                Birthday = user.BIRTHDAY,
                CountryID = user.COUNTRY_ID,
                MobileNumber = user.MOBILE_NO,
                Gender = user.GENDER,
                ProfilePicture = user.PROFILE_PIC,
                AboutMe = user.ABOUT_ME
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

        public static POST ToPOSTFromDB(Post post)
        {
            return new POST()
            {
                CONTENT = post.Content,
                PROFILE_OWNER_ID = post.ProfileOwnerID,
                POSTER_ID = post.PosterID
            };
        }

        public static Post ToListOfPost(VWUserProfilePost post)
        {
            return new Post()
            {
                Content = post.CONTENT,
                DateCreated = post.CREATED_DATE,
                ProfileOwnerID = post.PROFILE_OWNER_ID,
                PosterID = post.POSTER_ID,
                Username = post.USER_NAME,
                FirstName = post.FIRST_NAME,
                LastName = post.LAST_NAME,
                ProfilePic = post.PROFILE_PIC

            };   
        }
    }
}