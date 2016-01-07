using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FriendReqRepository
    {
        public static bool CheckActiveFriendRequests(User ActiveUser)
        {
            var friendList = new List<Friend>();
            using (var db = new FindLoveDbEntities())
            {
                var result = from r in db.Friend
                    where r.SenderId == ActiveUser.Id && r.Accepted == false
                    select r;
                foreach (var item in result)
                {
                    friendList.Add(item);
                }
                var returnboolean = friendList.Count > 0;
                return returnboolean;
            }

        }

        public static List<User> returnAllPendingRequestUsers(User ActiveUser)
        {
            var returnList = new List<User>();
            using (var db = new FindLoveDbEntities())
            {
                var result = from r in db.Friend
                    where r.ReceiverId == ActiveUser.Id && r.Accepted == false
                    select r;
                var UserRep = new UserRepository();
                foreach (var item in result)
                {
                    returnList.Add(UserRep.GetUserById(item.SenderId));
                }
            }
            return returnList;
        }

        public static void SendFriendRequest(User sender, User Reciever)
        {

            var friendReq = new Friend();
            friendReq.SenderId = sender.Id;
            friendReq.ReceiverId = Reciever.Id;
            friendReq.Accepted = false;
            using (var db = new FindLoveDbEntities())
            {
                db.Friend.Add(friendReq);
                db.SaveChanges();
            }
        }

        public static void AcceptOrDenyRequests(int sender, int reciever, bool accepted)
        {
            using (var db = new FindLoveDbEntities())
            {
                var currentRequest = db.Friend.FirstOrDefault(x => x.ReceiverId == reciever && x.SenderId == sender);

                if (accepted)
                {
                    currentRequest.Accepted = true;
                    db.SaveChanges();
                }
                else
                {
                    db.Friend.Remove(currentRequest);
                    db.SaveChanges();
                }
            }
        }
    }
}


