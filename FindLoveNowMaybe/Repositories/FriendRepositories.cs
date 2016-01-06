using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FriendRepositories
    {

        public static List<User> ReturnAllFriends(User ActiveUser)
        {
            var returnList = new List<User>();

            using (var db = new FindLoveDbEntities())
            {
                var result = from r in db.Friend
                             where r.ReceiverId == ActiveUser.Id || r.SenderId == ActiveUser.Id && r.Accepted == true
                             select r;
                var UserRep = new UserRepository();
            
            foreach (var item in result)
                {
                    if(item.ReceiverId == ActiveUser.Id)
                    {
                        returnList.Add(UserRep.GetUserById(item.SenderId));
                    }
                    else
                    {
                        returnList.Add(UserRep.GetUserById(item.ReceiverId));
                    }
                }
            }

            return returnList;

        }


    }
}
