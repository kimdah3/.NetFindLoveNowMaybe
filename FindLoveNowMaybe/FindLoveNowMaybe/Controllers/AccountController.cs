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
    [HandleError(ExceptionType = typeof(HttpException), View = "ErrorDbInternal")]
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
                return RedirectToAction("Profile", "User");
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegistrationModel());
        }

        [HttpPost]
        [HandleError(ExceptionType = typeof(DbUpdateException), View = "ErrorDbUpdate")]
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
                Picture = "default.png",
                Sex = model.Sex,
                InterestedMen = model.InterestedMen,
                InterestedWomen = model.InterestedWomen,
                Active = true
            };

            using (var userRepository = new UserRepository())
            {
                if (!userRepository.IsUniqueUserName(newUser.UserName))
                {
                    ModelState.AddModelError("", "Username already exists!");
                    return View();
                }
                userRepository.AddUser(newUser);
            }
            FormsAuthentication.SetAuthCookie(model.UserName, false);
            return RedirectToAction("Index", "Account");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}