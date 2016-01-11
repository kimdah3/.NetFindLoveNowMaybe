using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FriendReqRepository : IDisposable
    {
        public FindLoveDbEntities Context { get; set; }

        public FriendReqRepository()
        {
            Context = new FindLoveDbEntities();
        }

        /* Checks if a user have any pending friend requests. Returns true if there is one or more requests. */
        public bool CheckActiveFriendRequests(User ActiveUser)
        {
            var friendList = new List<Friend>();
            var result = from r in Context.Friend
                         where r.SenderId == ActiveUser.Id && r.Accepted == false
                         select r;
            foreach (var item in result)
            {
                friendList.Add(item);
            }
            return friendList.Count > 0;


        }

        //public List<User> returnAllPendingRequestUsers(User ActiveUser)
        //{
        //    var returnList = new List<User>();
        //    using (var db = new FindLoveDbEntities())
        //    {
        //        var result = from r in Context.Friend
        //                     where r.ReceiverId == ActiveUser.Id && r.Accepted == false
        //                     select r;
        //        using (var UserRep = new UserRepository())
        //        {
        //            foreach (var item in result)
        //            {
        //                returnList.Add(UserRep.GetUserById(item.SenderId));
        //            }
        //        }
        //    }
        //    return returnList;
        //}

        public List<Friend> returnAllPendingRequestsByUser(User ActiveUser)
        {

            var result = from f in Context.Friend.Include("SenderUser")
                         where f.ReceiverId == ActiveUser.Id
                               && f.Accepted == false
                         select f;

            return result.ToList();
        }

        public void SendFriendRequest(User sender, User Reciever)
        {

            var friendReq = new Friend();
            friendReq.SenderId = sender.Id;
            friendReq.ReceiverId = Reciever.Id;
            friendReq.Accepted = false;
            Context.Friend.Add(friendReq);
            Context.SaveChanges();
        }

        public void AcceptOrDenyRequests(int sender, int receiver, bool accepted)
        {
            var currentRequest = Context.Friend.FirstOrDefault(x => x.ReceiverId == receiver && x.SenderId == sender);

            if (accepted)
            {
                currentRequest.Accepted = true;
                Context.SaveChanges();
            }
            else
            {
                Context.Friend.Remove(currentRequest);
                Context.SaveChanges();
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}


