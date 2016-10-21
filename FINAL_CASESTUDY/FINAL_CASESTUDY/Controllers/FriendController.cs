using PastebookBusinessLogic.BusinessLogic;
using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FINAL_CASESTUDY.Controllers
{
    public class FriendController : Controller
    {
        FriendsBL friendBL = new FriendsBL();
        AccountBL accountBL = new AccountBL();

        // GET: Friend
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

        public ActionResult GetFriendList(string username)
        {
            int userID = (int)Session["currentUser"];
            List<USER> listOfFriends = friendBL.RetrieveFriends(userID);

            return PartialView("PartialFriendList", listOfFriends);
        }

        public ActionResult CheckIfRequestExist(int currentProfileID)
        {
            int userID = (int)Session["currentUser"];
            bool exist = friendBL.CheckIfRequestSent(userID, currentProfileID);
            return Json(new { request = exist }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckFriendUser(int currentProfileID)
        {
            int userID = (int)Session["currentUser"];
            bool userIsUser = friendBL.CheckFriendUser(userID, currentProfileID);
            return Json(new { checkUser = userIsUser }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckIfFriends(int currentProfileID)
        {
            int userID = (int)Session["currentUser"];
            bool friends = friendBL.CheckIfFriends(userID, currentProfileID);
            return Json(new { users = friends }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddAsFriend(int currentProfileID)
        {
            int userID = (int)Session["currentUser"];
            bool friends = friendBL.AddFriendRequest(userID, currentProfileID);
            return Json(new { users = friends }, JsonRequestBehavior.AllowGet);
        }
    }
}