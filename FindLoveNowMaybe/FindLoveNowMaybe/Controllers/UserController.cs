using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
                var allFriends = FriendRepositories.ReturnAllFriends(userRep.GetUserByUserName(User.Identity.Name));

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

            using (var friendReqRepository = new FriendReqRepository())
            {
                model.FriendReqs =
                    friendReqRepository.returnAllPendingRequestsByUser(userRep.GetUserByUserName(User.Identity.Name));

            }


            //var allFriendRequests = FriendReqRepository.returnAllPendingRequestUsers(userRep.GetUserByUserName(User.Identity.Name));

            //foreach (var i in allFriendRequests)  
            //{
            //    model.Add(i);
            //}

            return View(model);
        }

        public ActionResult IndexUser()
        {
            var homeModel = new HomeModel();
            using (var userRepository = new UserRepository())
            {
                homeModel.RandomUsers = userRepository.GetFourRandomUsers();
            }

            return View(homeModel);
        }

        public ActionResult VisitUser(string visitUser)
        {
            var userRepository = new UserRepository();
            var visitingUser = userRepository.GetUserByUserName(visitUser);

            var isFriends = FriendRepositories.CheckIfUsersAreFriends(User.Identity.Name, visitUser);

            var visitModel = new VisitModel()
            {
                UserName = visitingUser.UserName,
                Age = visitingUser.Age,
                Description = visitingUser.Description,
                FirstName = visitingUser.FirstName,
                InterestedMen = visitingUser.InterestedMen,
                InterestedWomen = visitingUser.InterestedWomen,
                LastName = visitingUser.LastName,
                Picture = visitingUser.Picture,
                Sex = visitingUser.Sex,
                IsFriends = isFriends
            };

            return View(visitModel);
        }

        public ActionResult AcceptRequest(int sender, int receiver)
        {
            using (var friendReqRepository = new FriendReqRepository())
            {
                friendReqRepository.AcceptOrDenyRequests(sender, receiver, true);
            }
            return RedirectToAction("FriendRequest");
        }

        public ActionResult DeclineRequest(int sender, int receiver)
        {
            using (var friendReqRepository = new FriendReqRepository())
            {
                friendReqRepository.AcceptOrDenyRequests(sender, receiver, false);
            }
            return RedirectToAction("FriendRequest");
        }

        public ActionResult SendFriendRequest(string senderUserName, string receiverUserName)
        {
            var senderUser = new User();
            var receiverUser = new User();
            using (var userRespo = new UserRepository())
            {
                senderUser = userRespo.GetUserByUserName(senderUserName);
                receiverUser = userRespo.GetUserByUserName(receiverUserName);
            }

            using (var friendReqRepository = new FriendReqRepository())
            {
                friendReqRepository.SendFriendRequest(senderUser,receiverUser);
            }

            return RedirectToAction("FriendRequest");
        }
    }


}