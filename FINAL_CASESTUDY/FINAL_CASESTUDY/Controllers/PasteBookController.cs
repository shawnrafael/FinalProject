using PastebookBusinessLogic.BusinessLogic;
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
        AccountBL accountBL = new AccountBL();
        CommentBL commentBL = new CommentBL();
        FriendsBL friendBL = new FriendsBL();
        LikeBL likeBL = new LikeBL();
        PostBL postBL = new PostBL();

        public ActionResult Register()
        {
            if (Session["currentUser"] == null)
            {
                ViewData["countryList"] = new SelectList(accountBL.GetCountryList(), "ID", "COUNTRY");
                return View();
            }
            else
            {
                return RedirectToAction("Home");
            }
        }

        public ActionResult ValidateRegister(USER user)
        {
            if (accountBL.CheckUser(user) == false)
            {
                ModelState.AddModelError("validateUser", "Username already exist");
            }
            if (ModelState.IsValid)
            {
                accountBL.Register(user);
                Session["currentUser"] = user.ID;
                Session["currentUserName"] = user.USER_NAME;
                return RedirectToAction("Home");
            }
            return RedirectToAction("Register", user);
        }

        public ActionResult ValidateLogin(string email, string password)
        {
            USER user = accountBL.LoginUser(email, password);
            bool loginSuccess = user.ID != 0;
            if (loginSuccess == true)
            {                
                Session["currentUser"] = user.ID;
                Session["currentUserName"] = user.USER_NAME;
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
            USER user = accountBL.GetUserByID(userID);
            return View(user);
        }

        public ActionResult GetPostOnHome()
        {
            int userID = (int)Session["currentUser"];

            List<USER> listOfUsers = friendBL.RetrieveFriends(userID);
            List<POST> listOfPost = postBL.RetrieveListOfPost(listOfUsers, userID);

            return PartialView("PartialPostOnProfile", listOfPost);
        }

        public ActionResult GetPostOnProfile(string username)
        {
            var user = accountBL.GetUserByUsername(username);
            List<POST> listOfPost = postBL.RetrieveListOfPostOnProfile(user.ID);

            return PartialView("PartialPostOnProfile", listOfPost);
        }

        public ActionResult GetLikers(int currentPost)
        {
            List<USER> listOfLikers = likeBL.DisplayLikers(currentPost);

            return PartialView("PartialLikeFromPost", listOfLikers);
        }

        public ActionResult Logout()
        {
            Session["currentUser"] = null;
            return RedirectToAction("Register");
        }

        public ActionResult AddPost(string content, int currentProfile)
        {
            if (Session["currentUser"] ==null)
            {
                return RedirectToAction("Register");
            }            
            int userID = (int)Session["currentUser"];
            currentProfile = (currentProfile == 0) ? userID : currentProfile;

            bool postSuccess = postBL.AddPost(content, userID, currentProfile);
            return Json(new { post = postSuccess }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserProfile(string username)
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Register");
            }
           
            USER user = accountBL.GetUserByUsername(username);                    
            return View(user);
        }

        public ActionResult Friends()
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Register");
            }
            int userID = (int)Session["currentUser"];
            List<USER> listOfFriends = friendBL.RetrieveFriends(userID);
            return View(listOfFriends);
        }

        public ActionResult LikePost(int currentPost)
        {
            int userID = (int)Session["currentUser"];
            bool liked = likeBL.LikePost(userID, currentPost);
            return Json(new { likePost = liked }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CommentPost(int currentPost, string commentContent)
        {
            int userID = (int)Session["currentUser"];
            bool posted = postBL.AddPost(commentContent, userID, currentPost);
            return Json(new { postAdded = posted }, JsonRequestBehavior.AllowGet);
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