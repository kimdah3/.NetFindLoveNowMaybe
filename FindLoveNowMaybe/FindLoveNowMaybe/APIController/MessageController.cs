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
    public class MessageController : ApiController
    {
        [HttpGet]
        public Models.MessageListModel GetMessages(string ActiveUser)
        {
            var userRepo = new Repositories.UserRepository();

            var modelList = new Models.MessageListModel();
            var allMessages = Repositories.MessageRepository.GetAllPostsForUser(userRepo.GetUserByUserName(ActiveUser));

            var messages = new List<MessageModel>();
            foreach (var m in allMessages)
            {
                var model = new Models.MessageModel();
                model.MessageId = m.Id;
                model.Message = m.Message1;
                model.SenderID = m.SenderId;
                model.RecieverID = m.ReceiverId;
                model.SenderFullName = userRepo.ReturnFullNameById(model.SenderID);
                messages.Add(model);
            }
            modelList.Message = messages;
            return modelList;
        }

        [HttpPost]
        public void PostMessage(string recieverUsername, string Message)
        {
            var userRepo = new Repositories.UserRepository();
            Repositories.MessageRepository.AddNewMessage(userRepo.GetUserByUserName(User.Identity.Name),
                userRepo.GetUserByUserName(recieverUsername).Id, Message);
        }

        [HttpGet]
        public string ReturnFullNameFromId(int id)
        {
            var userRepo = new Repositories.UserRepository();
            var fullName = userRepo.ReturnFullNameById(id);
            return fullName;
        }

        [HttpPost]
        public void RemoveMessage(int messageId)
        {
            MessageRepository.RemoveMessage(messageId);
        }
    }
}
