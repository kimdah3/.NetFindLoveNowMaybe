using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindLoveNowMaybe.Models;
using Repositories;

namespace FindLoveNowMaybe.Controllers
{

    [Authorize]
    public class EditController : Controller
    {
        // GET: Edit
        public ActionResult EditUser()
        {
            
            return View();
        }



    }
}

