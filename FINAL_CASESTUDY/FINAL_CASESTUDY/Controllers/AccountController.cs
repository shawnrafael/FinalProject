using PastebookBusinessLogic.BusinessLogic;
using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FINAL_CASESTUDY.Controllers
{
    public class AccountController : Controller
    {
        AccountBL accountBL = new AccountBL();
        // GET: Account
        public ActionResult Register()
        {
            if (Session["currentUser"] == null)
            {
                ViewData["countryList"] = new SelectList(accountBL.GetCountryList(), "ID", "COUNTRY");
                return View();
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }
        }

        public ActionResult CheckUsername(string username)
        {
            bool existing = accountBL.CheckUsername(username);

            return Json(new { userExist = existing }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidateRegister(USER user)
        {
            if (accountBL.CheckUsername(user.USER_NAME) == true)
            {
                ModelState.AddModelError("validateUser", "Username already exist");
            }

            if (accountBL.CheckEmail(user.EMAIL_ADDRESS) == true)
            {
                ModelState.AddModelError("validateEmail", "Email already exist");
            }
            if (ModelState.IsValid)
            {
                accountBL.Register(user);
                Session["currentUser"] = user.ID;
                Session["currentUserName"] = user.USER_NAME;
                return RedirectToAction("UserProfile","Profile",new { username = user.USER_NAME });
            }
            ViewData["countryList"] = new SelectList(accountBL.GetCountryList(), "ID", "COUNTRY");
            return View("Register", user);
        }

        public ActionResult ValidateLogin(string email, string password)
        {
            USER user = accountBL.LoginUser(email, password);
            bool loginSuccess = user != null;
            if (loginSuccess == true)
            {
                Session["currentUser"] = user.ID;
                Session["currentUserName"] = user.USER_NAME;
                
            }

            return Json(new { login = loginSuccess }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult UploadProfilePic(HttpPostedFileBase file)
        {
            var user = accountBL.GetUserByID((int)Session["currentUser"]);
            if (file != null)
            {                               
                bool success = accountBL.UploadProfilePic(file, user);
            }
            return RedirectToAction("UserProfile", "Profile", new { username = user.USER_NAME });
        }

        public ActionResult UpdateAboutMe(string aboutMe)
        {
            var user = accountBL.GetUserByID((int)Session["currentUser"]);
            bool valid = aboutMe.Length <= 2000 ;
            if (valid)
            {
                bool success = accountBL.UpdateAboutMe(aboutMe, user);
            }
            
            return RedirectToAction("UserProfile", "Profile", new { username = user.USER_NAME });
        }

        public ActionResult Setting()
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Register");
            }
            return View();
        }

        public ActionResult EditProfile()
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Register");
            }
            ViewData["countryList"] = new SelectList(accountBL.GetCountryList(), "ID", "COUNTRY");
            var user = accountBL.GetUserByID((int)Session["currentUser"]);
            return View("EditProfile", user);
        }

        public ActionResult UpdateProfileInfo(USER user)
        {
            if (user.GENDER == null)
            {
                user.GENDER = "U";
            }
            if (ModelState.IsValidField("MOBILE_NO"))
            {
                var currentUser = accountBL.GetUserByID((int)Session["currentUser"]);
                currentUser.BIRTHDAY = user.BIRTHDAY;
                currentUser.COUNTRY_ID = user.COUNTRY_ID;
                currentUser.USER_NAME = user.USER_NAME;
                currentUser.MOBILE_NO = user.MOBILE_NO;
                currentUser.LAST_NAME = user.LAST_NAME;
                currentUser.GENDER = user.GENDER;
                currentUser.FIRST_NAME = user.FIRST_NAME;

                bool success = accountBL.UpdateProfileInfo(currentUser);
                if (success)
                {
                    Session["currentUserName"] = user.USER_NAME;
                }
                return RedirectToAction("EditProfile", user);
            }

            ViewData["countryList"] = new SelectList(accountBL.GetCountryList(), "ID", "COUNTRY");
            return View("EditProfile", user);
        }

        public ActionResult ChangePassword()
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Register");
            }

            var correctPass = (TempData["checkPassword"] != null) ? (bool)TempData["checkPassword"] : true;
            ViewData["success"] = (TempData["success"] != null) ? (bool)TempData["success"] : false;

            if (correctPass == false)
            {
                ModelState.AddModelError("errorPassword", "Invalid password");
            }
            
            var user = accountBL.GetUserByID((int)Session["currentUser"]);
            return View("ChangePassword",user);
        }

        public ActionResult UpdateNewPassword(string oldPassword, USER user)
        {
            bool checkPassword = accountBL.CheckPassword(oldPassword, (int)Session["currentUser"]);
            TempData["checkPassword"] = checkPassword;

            bool success = false;
            if (checkPassword)
            {
                success = accountBL.UpdatePassword(user.PASSWORD, (int)Session["currentUser"]);
            }
            TempData["success"] = success;

            return RedirectToAction("ChangePassword");
        }

        public ActionResult ChangeEmail()
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Register");
            }

            var correctPass =(TempData["checkPassword"] != null) ? (bool)TempData["checkPassword"] : true;
            ViewData["success"] = (TempData["success"] != null) ? (bool)TempData["success"] : false;

            if (correctPass==false)
            {
                ModelState.AddModelError("errorPassword", "Invalid password");
            }

            var user = accountBL.GetUserByID((int)Session["currentUser"]);
            return View("ChangeEmail", user);
        }

        public ActionResult CheckEmail(string email)
        {
            bool exist = accountBL.CheckEmail(email);
            return Json(new { emailExist = exist }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateNewEmail(USER user)
        {
            bool checkPassword = accountBL.CheckPassword(user.PASSWORD, (int)Session["currentUser"]);
            TempData["checkPassword"] = checkPassword;

            bool success = false;            
            if (checkPassword)
            {
                success = accountBL.UpdateEmail(user.EMAIL_ADDRESS, (int)Session["currentUser"]);                
            }
            TempData["success"] = success;
            return RedirectToAction("ChangeEmail");

        }

        public ActionResult Logout()
        {
            Session["currentUser"] = null;
            Session["currentUserName"] = null;
            return RedirectToAction("Home", "Home");
        }       

    }
}