using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
                    Picture = activeUser.Picture,
                    Age = activeUser.Age,
                    Sex = activeUser.Sex,
                    InterestedMen = activeUser.InterestedMen,
                    InterestedWomen = activeUser.InterestedWomen
                };
            }
            return View(model);
        }

        public ActionResult Friends(string username)
        {
            var model = new FriendsModel();
            if (!User.Identity.Name.Equals(""))
            {
               
                var currentuser = new UserRepository();

                var userRep = new UserRepository();
                var allFriends = FriendRepositories.ReturnAllFriends(userRep.GetUserByUserName("Linus"));

                foreach (var i in allFriends) //loopar och hämtar namn och bild på varje vän 
                {
                    model.Add(i);
                    
                }
            }
            return View(model);

        }

        public ActionResult FriendRequest()
        {
            var model = new FriendRequestModel();
            var userRep = new UserRepository();
            var allFriendRequests = FriendReqRepository.returnAllPendingRequestUsers(userRep.GetUserByUserName("Linus"));

            foreach (var i in allFriendRequests)
            {
                model.Add(i);
            }

            return View(model);
        }
    }

   
}