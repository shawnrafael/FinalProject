using DataAccess.AccessLayer;
using PastebookBusinessLogic.BusinessLogic;
using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace FINAL_CASESTUDY.Controllers
{
    public class PostController : Controller
    {
        PostBL postBL = new PostBL();
        LikeBL likeBL = new LikeBL();
        CommentBL commentBL = new CommentBL();
        NotificationBL notifyBL = new NotificationBL();
        PasteBookAccessLayer pasteBookAL = new PasteBookAccessLayer();
        // GET: Post
        public ActionResult AddPost(string content, int currentProfile)
        {
            content = Regex.Replace(content, @"\s+", " ");
            content = content.Trim();
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Register");
            }
            int userID = (int)Session["currentUser"];
            currentProfile = (currentProfile == 0) ? userID : currentProfile;
            bool postSuccess = false;
            bool validPostCount = content.Length <= 1000;
            if (validPostCount)
            {
                postSuccess  = postBL.AddPost(content, userID, currentProfile);
            }            
            return Json(new { post = postSuccess }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLikers(int currentPost)
        {
            List<USER> listOfLikers = likeBL.DisplayLikers(currentPost);

            return PartialView("PartialLikeFromPost", listOfLikers);
        }

        public ActionResult LikePost(int currentPost)
        {
            int userID = (int)Session["currentUser"];
            var like = likeBL.LikePost(userID, currentPost);
            bool notify = false;
            if (like.ID != 0)
            {
                notify = notifyBL.AddNotification(like);
            }

            return Json(new { likePost = like!=null }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UnlikePost(int currentPost)
        {
            int userID = (int)Session["currentUser"];
            var likeNotif = notifyBL.RetrieveLikeNotif(userID, currentPost);
            var unlike = likeBL.UnlikePost(userID, currentPost);
            bool notify = false;
            if (unlike)
            {
                notify = notifyBL.DeleteNotification(likeNotif);
            }

            return Json(new { likePost = unlike }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CommentPost(int currentPost, string commentContent)
        {
            commentContent = Regex.Replace(commentContent, @"\s+", " ");
            commentContent = commentContent.Trim();
            int userID = (int)Session["currentUser"];
            bool validContent = commentContent.Length <= 1000;
            var comment = new COMMENT();

            if (validContent)
            {
                comment = commentBL.AddComment(commentContent, userID, currentPost);
            }

            bool addNotification = false;
            if (comment.ID != 0)
            {
                addNotification = notifyBL.AddNotification(comment);
            }
            return Json(new { postAdded = comment!=null, notify = addNotification }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CommentList(int postID)
        {
            List<COMMENT> listOfComment = commentBL.RetrieveComments(postID);
            return PartialView("PartialCommentList", listOfComment);
        }

        public ActionResult GetNotification()
        {
            int user = (int)Session["currentUser"];
            var notifications = notifyBL.RetrieveNotifications(user);

            return PartialView("PartialNotification", notifications);
        }

        public ActionResult GetNotificationCount(int userID)
        {
            int notificationCount = notifyBL.RetrieveNotificationCount(userID);
            return Json(new { notifCount = notificationCount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateNotificatioin(int userID)
        {
            bool updated = notifyBL.UpdateSeen(userID);
            return Json(new { notifUpdated = updated }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckNotification(int notifID, string notifMessage)
        {
            var notif = pasteBookAL.RetrieveNotification(notifID);
            int postID = (int)notif.POST_ID;
            TempData["notifMessage"] = notifMessage;
            return RedirectToAction("Post", new { postID = postID });

        }

        [Route("posts/{postID}")]
        public ActionResult Post(int postID)
        {
            var user = pasteBookAL.RetrieveUser((int)Session["currentUser"]);
            TempData["postID"] = postID;
            return View(user);
        }

        public ActionResult GetPost()
        {
            var post = pasteBookAL.RetrienvePost((int)TempData["postID"]);
            ViewData["postMessage"] = (TempData["notifMessage"] != null) ? TempData["notifMessage"].ToString() : "";
            return PartialView("PartialViewPost",post);
        }
    }
}