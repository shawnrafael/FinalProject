using FINAL_CASESTUDY.Managers;
using FINAL_CASESTUDY.Models;
using PasteBookEntity;
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
        PostManager postManager = new PostManager();
        // GET: PasteBook
        //public ActionResult Home()
        //{
        //    if (Session["loginUser"] == null)
        //    {
        //        return RedirectToAction("Register");
        //    }
        //    string loginUser = Session["loginUser"].ToString();
        //    User user = manager.GetUserByUsername(loginUser);
        //    return View(user);
        //}       

        public ActionResult Register()
        {
            if (Session["loginUser"] ==null)
            {
                Session["CountryList"] = new SelectList(manager.GetCountryList(), "CountryID", "CountryName");
                return View();
            }
            else
            {
                return RedirectToAction("Home","Profile");
            }
        }

        public ActionResult ValidateLogin(string email, string password)
        {
            bool loginSuccess = manager.LoginUser(email, password);
            if (loginSuccess == true)
            {
                Session["loginUser"] = manager.GetUserByEmail(email).Username;
            }

            return Json(new { login = loginSuccess }, JsonRequestBehavior.AllowGet);       
        }

        public ActionResult ValidateRegister(User user)
        {
            if (ModelState.IsValid)
            {
                manager.RegisterUser(user);
                Session["loginUser"] = user.Username;                
                return RedirectToAction("Home","Profile");
            }
            return View("Register", user);
        }

        public ActionResult AddPost(string content)
        {
            if (Session["loginUser"]==null)
            {
                return RedirectToAction("Register");
            }
            string loginUser = Session["loginUser"].ToString();
            USER poster = postManager.PostOnOwnProfile(loginUser);
            Post post = new Post()
            {
                Content = content,
                PosterID = poster.ID,
                ProfileOwnerID = poster.ID
            };
            bool postSuccess = postManager.CreatePost(post);
            return Json(new { post = postSuccess }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult UserProfile(string username)
        {
            if (Session["loginUser"] == null)
            {
                return RedirectToAction("Register");
            }
            string loginUser = Session["loginUser"].ToString();
            User user = manager.GetUserByUsername(loginUser);
            return View(user);
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