using FINAL_CASESTUDY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FINAL_CASESTUDY.Managers;

namespace FINAL_CASESTUDY.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        Manager manager = new Manager();
        PostManager postManager = new PostManager();

        public ActionResult Home()
        {
            if (Session["loginUser"] == null)
            {
                return RedirectToAction("Register","PasteBook");
            }
                string loginUser = Session["loginUser"].ToString();
                User user = manager.GetUserByUsername(loginUser);
                return View(user);
        }
        public ActionResult GetPostOnWall()
        {
            if (Session["loginUser"] == null)
            {
                return RedirectToAction("Register", "PasteBook");
            }
            var user = manager.GetUserByUsername(Session["loginUser"].ToString());
            List<Post> listOfPost = postManager.DisplayListOfPost(user.UserID);
            return PartialView("PartialPost", listOfPost);
        }
    }

}