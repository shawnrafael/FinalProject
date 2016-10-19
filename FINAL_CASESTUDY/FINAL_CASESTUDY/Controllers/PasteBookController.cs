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
        AccountManager accountManager = new AccountManager();
        PostManager postManager = new PostManager();
        FriendManager friendManager = new FriendManager();

        public ActionResult Register()
        {
            if (Session["currentUser"] == null)
            {
                Session["CountryList"] = new SelectList(accountManager.GetCountryList(), "CountryID", "CountryName");
                return View();
            }
            else
            {
                return RedirectToAction("Home");
            }
        }

        public ActionResult ValidateRegister(User user)
        {
            if (ModelState.IsValid)
            {
                accountManager.RegisterUser(user);
                Session["currentUser"] = user.UserID;
                Session["currentUserName"] = user.Username;
                return RedirectToAction("Home");
            }
            return View("Register", user);
        }

        public ActionResult ValidateLogin(string email, string password)
        {
            User user = accountManager.LoginUser(email, password);
            bool loginSuccess = user.UserID != 0;
            if (loginSuccess == true)
            {                
                Session["currentUser"] = user.UserID;
                Session["currentUserName"] = user.Username;
            }
            return Json(new { login = loginSuccess }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Home()
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Register");
            }
            int userID = (int)Session["currentUser"];
            User user = accountManager.GetUserByID(userID);
            return View(user);
        }

        public ActionResult GetPostOnHome()
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Register");
            }

            int userID = (int)Session["currentUser"];
            List<User> listOfUsers = friendManager.GetFriends(userID);

            List<Post> listOfPost = postManager.DisplayListOfPostOnHome( listOfUsers , userID);

            return PartialView("PartialPostOnHome", listOfPost);
        }

        public ActionResult GetPostOnProfile(string username)
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Register");
            }
            List<Post> listOfPost = postManager.DisplayListOfPostOnWall(username);

            return PartialView("PartialPostOnProfile", listOfPost);
        }

        public ActionResult Logout()
        {
            Session["currentUser"] = null;
            return RedirectToAction("Register");
        }

        public ActionResult AddPost(string content)
        {
            if (Session["currentUser"] ==null)
            {
                return RedirectToAction("Register");
            }
            int userID = (int)Session["currentUser"];
            USER poster = postManager.PostOnOwnProfile(userID);
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
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Register");
            }
            Session["currentUserName"] = username;
            User user = accountManager.GetUserByUsername(username);

                    
            return View(user);
        }

        public ActionResult Friends()
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Register");
            }
            int userID = (int)Session["currentUser"];
            List<User> listOfFriends = friendManager.GetFriends(userID);
            return View(listOfFriends);
        }

        public ActionResult LikePost(int currentPost)
        {
            int userID = (int)Session["currentUser"];
            bool liked = postManager.LikePost(userID, currentPost);
            return Json(new { likePost = liked }, JsonRequestBehavior.AllowGet);
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