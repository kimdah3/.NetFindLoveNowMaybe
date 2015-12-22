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
            using (var repostiry = new UserRepository())
            {
                var user = repostiry.GetFirstUser();
                var user2 = repostiry.GetUser(2);
                user.SentFriendRequest.Add(user2);
                repostiry.Save();
            }

            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegistrationModel());
        }

        [HttpPost]
        public ActionResult Register(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    Password = model.Password,
                    Description = model.Description,
                    Age = model.Age,
                    Picture = model.Picture,
                    Sex = model.Sex,
                    InterestedMen = model.InterestedMen,
                    InterestedWomen = model.InterestedWomen
                };

                using (var userRepository = new UserRepository())
                {
                    userRepository.Context.User.Add(newUser);
                    userRepository.Save();
                }
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
            //var registrationCompleted = true; // Försök registrera...
            //if (!registrationCompleted)
            //{
            //    ModelState.AddModelError("", "Ett felmeddelande...");
            //    return View();
            //}

            //FormsAuthentication.SetAuthCookie(model.UserName, false);
            //return RedirectToAction("Index", "User");
        }

        public ActionResult Login()
        {
            return View();
        }

    }
}