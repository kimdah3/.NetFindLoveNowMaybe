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
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Profile(string userName)
        {
            var model = new UserModel();
            using (var userRepository = new UserRepository())
            {
                var activeUser = userRepository.GetUserByUserName(userName);
                if (activeUser == null) return View();
                model = new UserModel()
                {
                    FirstName = activeUser.FirstName,
                    LastName = activeUser.LastName,
                    UserName = activeUser.UserName,
                    Description = activeUser.Description,
                    Age = activeUser.Age,
                    Sex = activeUser.Sex,
                    InterestedMen = activeUser.InterestedMen,
                    InterestedWomen = activeUser.InterestedWomen
                };
            }
            return View(model);
        }
    }
}