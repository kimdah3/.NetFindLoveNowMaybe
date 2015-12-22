using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using FindLoveNowMaybe.Models;
using Repositories;

namespace FindLoveNowMaybe.Controllers
{
    public class AccountController : Controller
    {

        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            //Resettar felmeddelande för login. 
            model.ErrorMessage = null;

            //Om felaktig input i validering, avbryt och retunera hela viewn
            if (!ModelState.IsValid) return View(model);

            var userName = model.UserName;
            var password = model.Password;

            using (var userRepository = new UserRepository())
            {
                var userLoggingIn = userRepository.LoginUser(userName, password);

                if (userLoggingIn == null) //avbryter och skickar felmeddelande om loginnet är felaktigt.
                {
                    model.ErrorMessage = "Wrong username or password"; //
                    return View(model);
                }
                FormsAuthentication.SetAuthCookie(userLoggingIn.UserName, false);
                return RedirectToAction("Profile", "User", new { userName = userLoggingIn.UserName});
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegistrationModel());
        }

        [HttpPost]
        public ActionResult Register(RegistrationModel model)
        {
            if (!ModelState.IsValid) return View(); //Om felaktig input, returnera view

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
            return RedirectToAction("Index", "Account");
        }
    }
}