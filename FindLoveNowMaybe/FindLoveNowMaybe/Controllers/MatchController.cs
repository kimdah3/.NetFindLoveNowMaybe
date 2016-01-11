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
            var model = new MatchesModel() //En modell med samling usermatchmodel som inhåller user och dess matchvalue
            {
                UserMatchesModel = new List<UserMatchModel>()
            };
            using (var userRepository = new UserRepository())
            {
                var random = new Random();
                var matchRepository = new MatchingRepository();
                var users = userRepository.GetAllUsers(userName);
                foreach (var user in users)
                {
                    model.UserMatchesModel.Add(new UserMatchModel()
                    {
                        MatchValue = matchRepository.MatchUsers(userName, user.UserName, random),
                        User = user
                    });
                }
            }

            //Sorterar listan efter matchvalue
            var ordered = model.UserMatchesModel.OrderByDescending(x => int.Parse(x.MatchValue)).ToList();
            model.UserMatchesModel = ordered;

            //Lägger til % för att fungera till vår css.
            foreach (var userMatchModel in model.UserMatchesModel)
            {
                userMatchModel.MatchValue += "%";
            }

            return View(model);
        }
    }

}