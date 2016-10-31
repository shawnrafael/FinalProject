using FINAL_CASESTUDY.Managers;
using FINAL_CASESTUDY.Models;
using PastebookBusinessLogic.BusinessLogic;
using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FINAL_CASESTUDY.Controllers
{
    public class AccountController : Controller
    {
        AccountBL accountBL = new AccountBL();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(USER user)
        {

            USER loginUser = accountBL.LoginUser(user.EMAIL_ADDRESS, user.PASSWORD);
            if (loginUser != null)
            {
                Session["currentUser"] = loginUser.ID;
                Session["currentUserName"] = loginUser.USER_NAME;                  
                return RedirectToAction("Home", "Home");
            }
            else
            {
                ModelState.AddModelError("errorMessageLogin", "Invalid email or password.");
            }

            return View("Login", user);
        }

        public ActionResult Register()
        {
            ViewData["countryList"] = new SelectList(accountBL.GetCountryList(), "ID", "COUNTRY");
            return View();
        }

        public ActionResult CheckUsername(string username)
        {
            bool existing = accountBL.CheckUsername(username);

            return Json(new { userExist = existing }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckEmail(string email)
        {
            bool exist = accountBL.CheckEmail(email);
            return Json(new { emailExist = exist }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ValidateRegister(USER user, string confirmPassword)
        { 
            if (accountBL.CheckUsername(user.USER_NAME) == true)
            {
                ModelState.AddModelError("USER_NAME", "Username already exist.");
            }

            if (accountBL.CheckEmail(user.EMAIL_ADDRESS) == true)
            {
                ModelState.AddModelError("EMAIL_ADDRESS", "Email already exist.");
            }

            if (confirmPassword == "")
            {
                ModelState.AddModelError("errorConfirm", "Confirm password field is required.");
            }

            if (confirmPassword != user.PASSWORD)
            {
                ModelState.AddModelError("errorConfirm", "Confirm password does not match.");
            }

            if (ModelState.IsValid)
            {
                accountBL.Register(user);
                Session["currentUser"] = user.ID;
                Session["currentUserName"] = user.USER_NAME;
                return RedirectToAction("UserProfile","Profile",new { username = user.USER_NAME });
            }
            ViewData["countryList"] = new SelectList(accountBL.GetCountryList(), "ID", "COUNTRY");
            return View("Register",user);
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
            aboutMe = Regex.Replace(aboutMe, @"\s+", " ");
            aboutMe = aboutMe.Trim();
            var user = accountBL.GetUserByID((int)Session["currentUser"]);
            bool valid = aboutMe.Length <= 2000 ;
            if (valid)
            {
                bool success = accountBL.UpdateAboutMe(aboutMe, user);
            }
            
            return RedirectToAction("UserProfile", "Profile", new { username = user.USER_NAME });
        }

        public ActionResult EditProfile()
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Register");
            }
            ViewData["success"] = (TempData["success"] != null) ? (bool)TempData["success"] : false;
           
            var user = accountBL.GetUserByID((int)Session["currentUser"]);

            EditProfileViewModel userInfoModel = Mapper.MapUSER(user);
            ViewData["countryList"] = new SelectList(accountBL.GetCountryList(), "ID", "COUNTRY");
            return View("EditProfile", userInfoModel);
        }

        [HttpPost]
        public ActionResult UpdateProfileInfo(EditProfileViewModel userInfoModel)
        {
            var currentUser = accountBL.GetUserByID((int)Session["currentUser"]);            

            if (accountBL.CheckUsername(userInfoModel.USER_NAME)==true && userInfoModel.USER_NAME.ToLower() != currentUser.USER_NAME.ToLower())
            {
                ModelState.AddModelError("USER_NAME", "Username already exist.");
            }

            if (ModelState.IsValid)
            {                
                currentUser.USER_NAME = userInfoModel.USER_NAME;
                currentUser.FIRST_NAME = userInfoModel.FIRST_NAME;
                currentUser.LAST_NAME = userInfoModel.LAST_NAME;
                currentUser.BIRTHDAY = userInfoModel.BIRTHDAY;
                currentUser.COUNTRY_ID = userInfoModel.COUNTRY_ID;                
                currentUser.MOBILE_NO = userInfoModel.MOBILE_NO;                
                currentUser.GENDER = userInfoModel.GENDER;                

                bool success = accountBL.UpdateProfileInfo(currentUser);
                if (success)
                {
                    Session["currentUserName"] = currentUser.USER_NAME;
                }
                TempData["success"] = success;
                return RedirectToAction("EditProfile", Mapper.MapUSER(currentUser));
            }

            ViewData["countryList"] = new SelectList(accountBL.GetCountryList(), "ID", "COUNTRY");
            ViewData["success"] = false;
            return View("EditProfile", userInfoModel);
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