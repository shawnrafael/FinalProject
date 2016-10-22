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

        public ActionResult CheckFriendRequest(int currentProfileID)
        {
            int userID = (int)Session["currentUser"];
            string status = friendBL.CheckFriendRequest(userID, currentProfileID);
            return Json(new { friend = status }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddAsFriend(int currentProfileID)
        {
            int userID = (int)Session["currentUser"];
            bool sent = friendBL.AddFriend(userID, currentProfileID);
            return Json(new { request = sent }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmRequest(int currentProfileID)
        {
            int userID = (int)Session["currentUser"];
            bool confirm = friendBL.ConfirmFriend(userID, currentProfileID);
            return Json(new { request = confirm }, JsonRequestBehavior.AllowGet);
        }

    }
}