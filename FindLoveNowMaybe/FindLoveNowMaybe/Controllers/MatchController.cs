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
    public class MatchController : Controller
    {
        // GET: Match
        public ActionResult Matches()
        {
            var userName = User.Identity.Name;
            var model = new MatchesModel()
            {
                UserMatchesModel = new List<UserMatchModel>()
            };
            using (var userRepository = new UserRepository())
            {
                var matchRepository = new MatchingRepository();
                var users = userRepository.GetAllUsers(userName);
                foreach (var user in users)
                {
                    model.UserMatchesModel.Add(new UserMatchModel()
                    {
                        MatchValue = matchRepository.MatchUsers(userName, user.UserName),
                        User = user
                    });
                }
            }

            return View(model);
        }
    }

}