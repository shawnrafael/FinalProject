using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FINAL_CASESTUDY.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult GlobalError()
        {
            return View("Error500");
        }

        public ViewResult NotFound()
        {
            Response.StatusCode = 404;  //you may want to set this to 200
            return View("Error404");
        }
    }
}