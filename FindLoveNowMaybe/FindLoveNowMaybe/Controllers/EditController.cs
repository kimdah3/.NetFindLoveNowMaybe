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
            var user = new User();
            using (var userRepository = new UserRepository())
            {
                user = userRepository.GetUserByUserName(User.Identity.Name);
            }

            var model = new EditModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Description = user.Description,
                Active = true,
                Visible = true,
                InterestedWomen = user.InterestedWomen,
                InterestedMen = user.InterestedMen
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(EditModel model)
        {
            if (!ModelState.IsValid) return View(); //Om felaktig input, returnera view


            var activeUser = new User();

            activeUser.FirstName = model.FirstName;
            activeUser.LastName = model.LastName;
            activeUser.UserName = User.Identity.Name; //Cookie username eftersom ingen input av username.
            activeUser.Password = model.Password;
            activeUser.Description = model.Description;
            activeUser.Age = model.Age;
            activeUser.Sex = model.Sex;
            activeUser.InterestedMen = model.InterestedMen;
            activeUser.InterestedWomen = model.InterestedWomen;
            activeUser.Active = model.Active;
            activeUser.Visible = model.Visible;

            var currentUser = User.Identity.Name;

            using (var userRepository = new UserRepository())
            {
                var editRepository = new EditRepository();
                editRepository.UpdatePerson(currentUser, activeUser);
                userRepository.Save();
            }
            return RedirectToAction("Profile", "User");
        }


    }
}

