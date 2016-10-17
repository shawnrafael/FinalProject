using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FINAL_CASESTUDY.Controllers
{
    public class PasteBookController : Controller
    {
        // GET: PasteBook
        public ActionResult Home()
        {
            if (true)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            return View();
        }

        public ActionResult Friends()
        {
            return View();
        }

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