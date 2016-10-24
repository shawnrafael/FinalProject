using PastebookBusinessLogic.BusinessLogic;
using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FINAL_CASESTUDY.Controllers
{   

    public class HomeController : Controller
    {
        AccountBL accountBL = new AccountBL();
        FriendsBL friendBL = new FriendsBL();
        PostBL postBL = new PostBL();
        // GET: Home
        public ActionResult Home()
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Register","Account");
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

            return PartialView("PartialPost", listOfPost);
        }

        
    }
}