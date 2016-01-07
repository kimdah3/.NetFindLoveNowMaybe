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
            var returnList = new List<Message>();

            using (var db = new FindLoveDbEntities())
            {
                var result = from i in db.Message
                             where i.ReceiverId == ActiveUser.Id
                             select i;
                return result.ToList();
            }
        }

        public static void AddNewPost(User Reciever, String Message)
        {

        }
    }
}
