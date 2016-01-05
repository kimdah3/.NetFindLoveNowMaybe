using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindLoveNowMaybe.Models;
using Repositories;
using System.Web.Security;

namespace FindLoveNowMaybe.Controllers
{

    [Authorize]
    public class EditController : Controller
    {
        [HttpGet]
        public ActionResult EditUser()
        {
            return View(new EditModel());
        }

        [HttpPost]           
       
            public ActionResult EditUser(EditModel model)
        {
            if (!ModelState.IsValid) return View(); //Om felaktig input, returnera view


            var activeUser = new User();

            activeUser.FirstName = model.FirstName;
            activeUser.LastName = model.LastName;
            activeUser.UserName = model.UserName;
            activeUser.Password = model.Password;
            activeUser.Description = model.Description;
            activeUser.Age = model.Age;
            activeUser.Sex = model.Sex;
            activeUser.InterestedMen = model.InterestedMen;
            activeUser.InterestedWomen = model.InterestedWomen;

            var currentUser = User.Identity.Name;

            using (var userRepository = new UserRepository())
            {
                if (!userRepository.IsUniqueUserName(activeUser.UserName))
                {
                    ModelState.AddModelError("", "Username already exists!");
                    return View();
                }
                var editRepository = new EditRepository();
                editRepository.UpdatePerson(currentUser, activeUser);
                userRepository.Save();
            }
            FormsAuthentication.SetAuthCookie(model.UserName, false);
            return RedirectToAction("Index", "Account");
        }
    }
}

