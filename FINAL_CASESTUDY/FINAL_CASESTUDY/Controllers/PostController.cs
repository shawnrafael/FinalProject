using PastebookBusinessLogic.BusinessLogic;
using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
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
        // GET: Post
        public ActionResult AddPost(string content, int currentProfile)
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Register");
            }
            int userID = (int)Session["currentUser"];
            currentProfile = (currentProfile == 0) ? userID : currentProfile;

            bool postSuccess = postBL.AddPost(content, userID, currentProfile);
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

        public ActionResult CommentPost(int currentPost, string commentContent)
        {
            int userID = (int)Session["currentUser"];
            var comment = commentBL.AddComment(commentContent, userID, currentPost);

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
            var user = (int)Session["currentUser"];
            var notifications = notifyBL.RetrieveNotifications(user);

            return PartialView("PartialNotification", notifications);
        }
    }
}