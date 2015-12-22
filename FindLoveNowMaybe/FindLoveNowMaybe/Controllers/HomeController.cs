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
            using(var repostiry = new UserRepository())
            {
                var user = repostiry.GetFirstUser();
                var user2 = repostiry.GetUser(2);
                user.SentFriendRequest.Add(user2);
                repostiry.SaveUser();
            }

            return View();
        }

        public ActionResult Register(RegistrationModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var registrationCompleted = true; // Försök registrera...
            if (!registrationCompleted)
            {
                ModelState.AddModelError("", "Ett felmeddelande...");
                return View();
            }
            FormsAuthentication.SetAuthCookie(model.UserName, false);
            return RedirectToAction("Index", "User");
        }
                public ActionResult Login()
        {
            return View();
        }
    }
}