using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindLoveNowMaybe.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult HttpError()
        {
            return View();
        }

        public ActionResult PageNotFound()
        {
            return RedirectToAction("HttpError", "Error");
        }

        public ActionResult ServerIssues()
        {
            return RedirectToAction("HttpError");
        }

        public ActionResult GeneralIssue()
        {
            return View();
        }
    }
}