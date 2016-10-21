using PastebookBusinessLogic.BusinessLogic;
using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FINAL_CASESTUDY.Controllers
{
    public class PasteBookController : Controller
    {
        AccountBL accountBL = new AccountBL();
        CommentBL commentBL = new CommentBL();
        FriendsBL friendBL = new FriendsBL();
        LikeBL likeBL = new LikeBL();
        PostBL postBL = new PostBL();

       

        

        

        

       

        

        

        

        

        

       

        

       

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult UserPost()
        {
            return View();
        }
    }
}