using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class MessageRepository
    {
        public static List<Message> GetAllPostsForUser(User ActiveUser)
        {
            using (var db = new FindLoveDbEntities())
            {
                var result = from i in db.Message
                             where i.ReceiverId == ActiveUser.Id
                             select i;
                return result.ToList();
            }
        }

        public static void AddNewMessage(User sender, int recieverId, string message)
        {
            using (var userRep = new UserRepository())
            {
                var senderId = sender.Id;
                using (var db = new FindLoveDbEntities())
                {
                    var newMessage = new Message();
                    newMessage.Message1 = message;
                    newMessage.ReceiverId = recieverId;
                    newMessage.SenderId = senderId;
                    db.Message.Add(newMessage);
                    db.SaveChanges();
                }
            }

        }

        public static void RemoveMessage(int MessageId)
        {
            using (var db = new FindLoveDbEntities())
            {
                var msg = db.Message.FirstOrDefault(x => x.Id == MessageId);
                db.Message.Remove(msg);
                db.SaveChanges();
            }
        }

    }
}
