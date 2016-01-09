using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FindLoveNowMaybe.Models;
using Repositories;

namespace FindLoveNowMaybe.APIController
{
    public class VisitController : ApiController
    {
        [HttpPost]
        public void UserVisiting(string visitorUserName, string visitedUserName)
        {
            var visitRepository = new VisitRepository();
            visitRepository.SaveVisits(visitorUserName, visitedUserName);
        }


        [HttpGet]
        public LastVisitedUsersModel LastVisitedUsers(string userName)
        {
            var visitRepository = new VisitRepository();
            var model = new LastVisitedUsersModel() { Visitors = new List<VisitorUserModel>() };
            var visitors = visitRepository.GetFiveLatestVisitors(userName);

            foreach (var visitor in visitors)
            {
                model.Visitors.Add(new VisitorUserModel()
                {
                    UserName = visitor.UserName,
                    FirstName = visitor.FirstName,
                    LastName = visitor.LastName
                });
            }

            return model;
        }

        //[HttpGet]
        //public Models.MessageListModel LastVisitedUsers(string ActiveUser)
        //{
        //    var userRepo = new Repositories.UserRepository();

        //    var modelList = new Models.MessageListModel();
        //    var allMessages = Repositories.MessageRepository.GetAllPostsForUser(userRepo.GetUserByUserName(ActiveUser));

        //    var messages = new List<MessageModel>();
        //    foreach (var m in allMessages)
        //    {
        //        var model = new Models.MessageModel();
        //        model.Message = m.Message1;
        //        model.SenderID = m.SenderId;
        //        model.RecieverID = m.ReceiverId;
        //        model.SenderFullName = userRepo.ReturnFullNameById(model.SenderID);
        //        messages.Add(model);
        //    }

        //    modelList.Message = messages;

        //    return modelList;
        //}

        //[HttpGet]
        //public LastVisitedUsersModel LastVisitedUsers(string ActiveUser)
        //{

        //    return new LastVisitedUsersModel() { Visitors = new List<User>()};
        //}

    }
}
