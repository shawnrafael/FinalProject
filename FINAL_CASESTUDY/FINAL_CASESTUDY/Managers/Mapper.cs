using FINAL_CASESTUDY.Models;
using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FINAL_CASESTUDY.Managers
{
    public static class Mapper
    {
        public static EditProfileViewModel MapUSER(USER user)
        {
            return new EditProfileViewModel()
            {
                USER_NAME = user.USER_NAME,
                FIRST_NAME = user.FIRST_NAME,
                LAST_NAME = user.LAST_NAME,
                BIRTHDAY = user.BIRTHDAY,
                COUNTRY_ID = user.COUNTRY_ID,
                MOBILE_NO = user.MOBILE_NO,
                GENDER = user.GENDER
            };
        }
    }
}