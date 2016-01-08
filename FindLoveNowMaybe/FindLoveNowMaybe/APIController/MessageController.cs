using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FindLoveNowMaybe.Models;

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

            //modelList.Message.Add(new MessageModel()
            //{
            //    Message = "hej",
            //    RecieverID = 1,
            //    SenderID = 2,
            //});

            var messages = new List<MessageModel>();
            foreach (var m in allMessages)
            {
                var model = new Models.MessageModel();
                model.Message = m.Message1;
                model.SenderID = m.SenderId;
                model.RecieverID = m.ReceiverId;
                messages.Add(model);
                //modelList.Message.Add(model);
            }

            modelList.Message = messages;

            return modelList;
        }

        [HttpPost]
        public void PostMessage(string RecieverUsername, string Message)
        {
            var userRepo = new Repositories.UserRepository();
            Repositories.MessageRepository.AddNewMessage(userRepo.GetUserByUserName(User.Identity.Name), userRepo.GetUserByUserName(RecieverUsername).Id, Message);
        }
    }
}
