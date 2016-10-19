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
                ID = user.UserID,
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
                ID = post.ID,
                CONTENT = post.Content,
                PROFILE_OWNER_ID = post.ProfileOwnerID,
                POSTER_ID = post.PosterID
            };
        }

        public static Post ToListOfPost(POST post, List<Like> listOfLikes)
        {
            return new Post()
            {
                ID = post.ID,
                Content = post.CONTENT,
                DateCreated = post.CREATED_DATE,
                ProfileOwnerID = post.PROFILE_OWNER_ID,
                PosterID = post.POSTER_ID,
                Username = post.USER.USER_NAME,
                FirstName = post.USER.FIRST_NAME,
                LastName = post.USER.LAST_NAME,
                ProfilePic = post.USER.PROFILE_PIC,
                ListOfLikes = listOfLikes

            };   
        }

        public static Friend ToFriend(FRIEND friend)
        {
            return new Friend()
            {
                ID = friend.ID,
                USER_ID = friend.USER_ID,
                FRIEND_ID = friend.FRIEND_ID,
                REQUEST = friend.REQUEST,
                BLOCKED = friend.BLOCKED,
                CREATED_DATE = friend.CREATED_DATE
            };
        }

        public static Like ToLike(LIKE like)
        {
            return new Like()
            {
                ID = like.ID,
                PostID = like.POST_ID,
                LikedBy = like.LIKED_BY
            };
        }
        //public static FRIEND ToFRIENDFromDB(Friend friend)
        //{
        //    return new FRIEND()
        //    {
        //        ID = friend.ID,
        //        USER_ID = friend.USER_ID,
        //        FRIEND_ID = friend.FRIEND_ID,
        //        REQUEST = friend.REQUEST,
        //        BLOCKED = friend.BLOCKED,
        //        CREATED_DATE = friend.CREATED_DATE
        //    };
        //}
    }
}