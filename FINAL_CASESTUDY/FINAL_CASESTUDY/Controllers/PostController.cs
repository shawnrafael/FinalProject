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
            bool liked = likeBL.LikePost(userID, currentPost);
            return Json(new { likePost = liked }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CommentPost(int currentPost, string commentContent)
        {
            int userID = (int)Session["currentUser"];
            bool posted = commentBL.AddComment(commentContent, userID, currentPost);
            return Json(new { postAdded = posted }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CommentList(int postID)
        {
            List<COMMENT> listOfComment = commentBL.RetrieveComments(postID);
            return PartialView("PartialCommentList", listOfComment);
        }
    }
}