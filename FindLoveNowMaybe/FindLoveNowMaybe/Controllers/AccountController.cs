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
    public class AccountController : Controller
    {

        // GET: Login
        [HttpPost]
        public ActionResult Index()
        {
            return View(new LoginModel());
        }

        [HttpGet]
        public ActionResult Index(LoginModel model)
        {
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
            if (!ModelState.IsValid) return View(); //Om felaktig input, returnera tom view

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