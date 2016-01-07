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
            var homeModel = new HomeModel();
            using (var userRepository = new UserRepository())
            {
                homeModel.RandomUsers = userRepository.GetFourRandomUsers();
            }

            return View(homeModel);
        }

      
    }
}