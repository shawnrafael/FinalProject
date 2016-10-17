using FINAL_CASESTUDY.Managers;
using FINAL_CASESTUDY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FINAL_CASESTUDY.Controllers
{
    public class PasteBookController : Controller
    {
        Manager manager = new Manager();
        // GET: PasteBook
        public ActionResult Home()
        {
            if (false)
            {
                return RedirectToAction("Register");
            }
            return View();
        }

        public ActionResult Register()
        {
            if (Session["CountryList"] == null)
            {
                Session["CountryList"] = new SelectList(manager.GetCountryList(), "CountryID", "CountryName");
            }
            return View();
        }

        public ActionResult ValidateLogin(string email, string password)
        {
            bool loginSuccess = manager.LoginUser(email, password);
            if (loginSuccess == true)
            {
                Session["user"] = email;
                return RedirectToAction("Home");
            }

            return Json(new { login = loginSuccess }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidateRegister(User user)
        {
            if (ModelState.IsValid)
            {
                manager.RegisterUser(user);
                Session["user"] = user.Username;                
                return RedirectToAction("Home");
            }
            return View("Register", user);
        }

        public ActionResult UserProfile()
        {
            return View();
        }

        public ActionResult Friends()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult UserPost()
        {
            return View();
        }
    }
}