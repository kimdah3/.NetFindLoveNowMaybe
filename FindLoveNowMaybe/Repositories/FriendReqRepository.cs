using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public static void SendFriendRequest(User sender, User Reciever)
    {
        Reciever.SentFriendRequest.Add(sender);
        sender.
        var friendreq = new Friendrequest();
        friendReq.FRequestSenderId = sender.ID;
        friendReq.FRequestRecieverId = receiver.ID;
        using (var db = new FindLoveDbEntities())
        {
            db.FriendRequest.Add(friendReq);
            db.SaveChanges();
        }
    }
}
