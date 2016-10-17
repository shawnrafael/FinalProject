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
            if (Session["username"]==null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            if (ViewBag.CountryList == null)
            {
                ViewBag.CountryList = new SelectList(manager.GetCountryList(), "CountryID", "CountryName");
            }
            return View();
        }

        public ActionResult ValidateLogin(User user)
        {
            if (ModelState.IsValid)
            {
                manager.RegisterUser(user);
                Session["username"] = user.Username;                
                return RedirectToAction("Home");
            }
            return View("Login", user);
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