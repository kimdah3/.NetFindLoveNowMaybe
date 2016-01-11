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
        public ActionResult Profile()
        {
            var userName = User.Identity.Name;
            var model = new UserModel();
            var amountOfFriendRequests = 0;

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
            if (TempData.ContainsKey("Serialized"))
            {
                ViewBag.Serialized = TempData["Serialized"].ToString();
            }
            ViewBag.AmountOfFriendReqs = amountOfFriendRequests;
            return View(model);
        }

        public ActionResult Friends()
        {
            var userName = User.Identity.Name;
            var model = new FriendsModel()
            {
                Friends = new List<User>(),
                FriendsInCategoryModels = new List<FriendsInCategoryModel>()
            };


            if (!userName.Equals(""))
            {

                var userRep = new UserRepository();
                var allFriends = FriendRepositories.ReturnAllFriends(userRep.GetUserByUserName(userName));
                var categoryRep = new CategoryRepository();
                var allCategories = categoryRep.ReturnAllFriendsWithCategory(userName);
                var allCategoriesForUser = categoryRep.GetAllCategoriesForUser(userName);
                foreach (var i in allFriends) //loopar och hämtar namn och bild på varje vän 
                {
                    if (categoryRep.IsFriendCategorised(userName, i.UserName))
                    {
                        model.Friends.Add(i);
                    }

                }
                foreach (var category in allCategoriesForUser)
                {
                    model.FriendsInCategoryModels.Add(new FriendsInCategoryModel()
                    {
                        Name = category,
                        Users = categoryRep.GetFriendsByCategory(userName,category)
                    });
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
            var matchRepo = new MatchingRepository();
            var matchIndex = matchRepo.MatchUsers(User.Identity.Name, visitUser);
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
                IsFriends = isFriends,
                matchIndex = matchIndex,
                isMatched = false

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
                friendReqRepository.SendFriendRequest(senderUser, receiverUser);
            }

            return RedirectToAction("FriendRequest");
        }

        public PartialViewResult GetAmountOfFriendRequests()
        {
            var model = new AmountOfFriendRequestsModel();
            var friendReqRepository = new FriendReqRepository();
            var user = new User();

            using (var userRepo = new UserRepository())
            {
                user = userRepo.GetUserByUserName(User.Identity.Name);
            }
            model.AmountPending = friendReqRepository.returnAllPendingRequestsByUser(user).Count;
            return PartialView(model);
        }

        public ActionResult SerializeUser()
        {
            var serializeRepo = new SerializeRepository();
            var userName = User.Identity.Name;
            serializeRepo.SerializeProfile(userName);

            TempData["serialized"] = "User serialized in my documents folder, filename: XML-Profile";

            return RedirectToAction("Profile");
        }

        [HttpPost]
        public ActionResult AddFriendToCategory(FriendsModel model, string friendUserName, string categoryName)
        {
            var categoryRepository = new CategoryRepository();
            var userName = User.Identity.Name;

            categoryRepository.AddCategoryToFriend(userName, friendUserName, categoryName);

            return RedirectToAction("Friends");
        }
    }
}

