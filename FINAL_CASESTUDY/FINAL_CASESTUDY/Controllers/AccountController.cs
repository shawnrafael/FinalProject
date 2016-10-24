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

        public ActionResult ValidateRegister(USER user)
        {
            if (accountBL.CheckUserName(user) == false)
            {
                ModelState.AddModelError("validateUser", "Username already exist");
            }
            if (ModelState.IsValid)
            {
                accountBL.Register(user);
                Session["currentUser"] = user.ID;
                Session["currentUserName"] = user.USER_NAME;
                return RedirectToAction("Home","Home");
            }
            return RedirectToAction("Register", user);
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
            if (aboutMe != null)
            {
                bool success = accountBL.UpdateAboutMe(aboutMe, user);
            }
            return RedirectToAction("UserProfile", "Profile", new { username = user.USER_NAME });
        }

        public ActionResult Logout()
        {
            Session["currentUser"] = null;
            return RedirectToAction("Register", "Account");
        }

        

    }
}