using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FindLoveNowMaybe.Models;
using Repositories;

namespace FindLoveNowMaybe.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //******************
            //KOD SOM TESTAR uTIFALL DET GÅR ATT SKICKA FRIENDREQUEST.
            //******************
            //using (var repostiry = new UserRepository())
            //{
            //    var user = repostiry.GetFirstUser();
            //    var user2 = repostiry.GetUser(2);
            //    user.SentFriendRequest.Add(user2);
            //    repostiry.Save();
            //}

            return View();
        }
    }
}