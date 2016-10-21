using PastebookBusinessLogic.BusinessLogic;
using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FINAL_CASESTUDY.Controllers
{
    public class ProfileController : Controller
    {
        AccountBL accountBL = new AccountBL();
        FriendsBL friendBL = new FriendsBL();
        PostBL postBL = new PostBL();
        // GET: Profile
        public ActionResult UserProfile(string username)
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Register","Account");
            }

            USER user = accountBL.GetUserByUsername(username);
            return View(user);
        }

        public ActionResult GetPostOnProfile(string username)
        {
            var userID = (int)Session["currentUser"];
            var profileOwner = accountBL.GetUserByUsername(username);
            var listOfPost = new List<POST>();

            var friends = friendBL.CheckIfFriends(userID, profileOwner.ID);
            if (friends || userID == profileOwner.ID)
            {
                listOfPost = postBL.RetrieveListOfPostOnProfile(profileOwner.ID);
                return PartialView("PartialPost", listOfPost);
            }
            else
            {
                ViewData["currentProfile"] = profileOwner.FIRST_NAME;
                return PartialView("PartialNothingToPost");
            }            

            
        }     
        
        public ActionResult ReloadProfileHeader(string username)
        {
            USER user = accountBL.GetUserByUsername(username);
            return PartialView("PartialProfileHeader",user);
        }


    }
}