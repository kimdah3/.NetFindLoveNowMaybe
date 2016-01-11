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
                             where (r.ReceiverId == ActiveUser.Id || r.SenderId == ActiveUser.Id) && r.Accepted
                             select r;
                using (var userRep = new UserRepository())
                {
                    foreach (var item in result)
                    {
                        if (item.ReceiverId == ActiveUser.Id)
                        {
                            var user = userRep.GetUserById(item.SenderId);
                            if (user.Active == true)
                            {
                                returnList.Add(user);
                            }
                        }
                        else
                        {
                            var user = userRep.GetUserById(item.ReceiverId);
                            if (user.Active == true)
                            {
                                returnList.Add(user);
                            }
                        }

                    }
                }
            }
            return returnList;

        }

        /* Returns true if two users are already friends */
        public static bool CheckIfUsersAreFriends(string SenderUserName, string RecieverUserName)
        {
            using (var userRep = new UserRepository())
            {

                var senderId = userRep.GetUserByUserName(SenderUserName).Id;
                var recieverId = userRep.GetUserByUserName(RecieverUserName).Id;

                using (var db = new FindLoveDbEntities())
                {
                    var result = from r in db.Friend
                                 where r.SenderId == senderId && r.ReceiverId == recieverId || r.SenderId == recieverId && r.ReceiverId == senderId
                                 select r;

                    return result.ToList().Count > 0;

                }
            }

        }


    }
}
