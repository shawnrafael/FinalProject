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
        NotificationBL notifyBL = new NotificationBL();

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
            //var getRequest = friendBL.GetFriendRequest(userID, currentProfileID);
            bool sent = friendBL.AddFriend(userID, currentProfileID);
            bool notify = false;
            if (sent)
            {
                notify = notifyBL.AddNotification(userID, currentProfileID);
            }

            return Json(new { request = sent }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmRequest(int currentProfileID)
        {
            int userID = (int)Session["currentUser"];
            bool confirm = friendBL.ConfirmFriend(userID, currentProfileID);

            var requestNotif = notifyBL.RetrieveFriendRequest(userID, currentProfileID);
            //bool notify = false;
            //if (confirm)
            //{
            //    notify = notifyBL.UpdateSeen(requestNotif);
            //}
            return Json(new { request = confirm }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteRequest(int currentProfileID)
        {
            int userID = (int)Session["currentUser"];
            var requestNotif = notifyBL.RetrieveFriendRequest(userID, currentProfileID);

            bool delete = friendBL.DeleteFriendRequest(userID, currentProfileID);            
            bool notify = false;
            if (delete)
            {
                notify = notifyBL.DeleteNotification(requestNotif);
            }
            return Json(new { request = delete }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetRequestList()
        //{
        //    var user = (int)Session["currentUser"];
        //    var requests = notifyBL.RetrieveRequests(user);

        //    return PartialView("PartialFriendRequest", requests);
        //}

        public ActionResult SearchResult(string searchFriend)
        {
            var searchResult = friendBL.SearchFriend(searchFriend);
            ViewData["keyWord"] = (searchFriend == null) ? "" : searchFriend;
            return View(searchResult);
        }
        
    }
}