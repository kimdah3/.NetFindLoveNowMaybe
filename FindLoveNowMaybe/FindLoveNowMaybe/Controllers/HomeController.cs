using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositories;

namespace FindLoveNowMaybe.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            using(var repostiry = new UserRepository())
            {
                var user = repostiry.GetFirstUser();
                var user2 = repostiry.GetUser(3);
                user.SentFriendRequests.Add(user2);
                repostiry.SaveUser();
            }
            

            return View();
        }
    }
}