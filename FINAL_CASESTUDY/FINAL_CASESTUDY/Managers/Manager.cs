using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PastebookBusinessLogic.BusinessLogic;
using FINAL_CASESTUDY.Models;

namespace FINAL_CASESTUDY.Managers
{
    public class Manager
    {
        PasteBookBL pasteBookBL = new PasteBookBL();
        public bool RegisterUser(User user)
        {
            if (user.Gender == null)
            {
                user.Gender = "U";
            }

            int result = pasteBookBL.Register(Mapper.ToUSER(user));
            return result != 0;
        }

        public List<Country> GetCountryList()
        {
            List<Country> listCountry = new List<Country>();
            foreach (var item in pasteBookBL.GetCountryList())
            {
                listCountry.Add(Mapper.ToCountryList(item));
            }
            return listCountry;
        }

        public bool LoginUser(string email, string password)
        {
            int result = pasteBookBL.LoginUser(email, password);
            return result != 0;
        }
    }
}