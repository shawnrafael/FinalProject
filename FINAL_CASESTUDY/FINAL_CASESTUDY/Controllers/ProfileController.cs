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

        [Route("{username:minlength(1)}")]
        public ActionResult UserProfile(string username)
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Register","Account");
            }
            ViewData["page"] = "profile";
            USER user = accountBL.GetUserByUsername(username);

            ViewData["status"] = friendBL.CheckFriendRequest((int)Session["currentUser"], user.ID);
            return View(user);
        }

        public ActionResult GetPostOnProfile(string username)
        {
            var userID = (int)Session["currentUser"];
            var profileOwner = accountBL.GetUserByUsername(username);
            var listOfPost = new List<POST>();

            var status = friendBL.CheckFriendRequest(userID, profileOwner.ID);

            if (status == "friends" || status == "profile owner")
            {
                listOfPost = postBL.RetrieveListOfPostOnProfile(profileOwner.ID);
                return PartialView("PartialPost", listOfPost);
            }            
            ViewData["currentProfile"] = profileOwner.FIRST_NAME;
            return PartialView("PartialNothingToPost");
 
        }

        [Route("friends")]
        public ActionResult Friends()
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Register", "Account");
            }
            int userID = (int)Session["currentUser"];
            USER user = accountBL.GetUserByID(userID);
            return View(user);
        }

    }
}